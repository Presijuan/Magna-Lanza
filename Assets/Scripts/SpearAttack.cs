using UnityEngine;
using System.Collections;

public class SpearAttack : MonoBehaviour
{
    public Transform spearRoot;     // Empty que contiene la lanza
    public float moveDistance = 0.5f; // cuánto avanza la lanza
    public float moveSpeed = 10f;     // qué tan rápido se mueve
    public BoxCollider spearCollider; // collider en la punta de la lanza

    // Rigidbody usado para mover la lanza (puede ser kinematic)
    private Rigidbody spearRigidbody;

    private bool isAttacking = false;
    public bool IsAttacking { get { return isAttacking; } } // propiedad pública de sólo lectura

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private void Awake()
    {
        if (spearRoot != null)
        {
            // Busca un Rigidbody en el mismo transform del spearRoot o en sus padres
            spearRigidbody = spearRoot.GetComponent<Rigidbody>() ?? spearRoot.GetComponentInParent<Rigidbody>();
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        Vector3 startPos = spearRoot.localPosition;
        Vector3 targetPos = startPos + Vector3.forward * moveDistance;

        // Activar el collider (desactivar trigger)
        if (spearCollider != null)
            spearCollider.isTrigger = false;

        // Avanzar
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            Vector3 newLocal = Vector3.Lerp(startPos, targetPos, t);

            if (spearRigidbody != null)
            {
                // Convertir la posición local a world según el padre del spearRoot (si existe)
                Vector3 worldPos = (spearRoot.parent != null) ? spearRoot.parent.TransformPoint(newLocal) : newLocal;
                spearRigidbody.MovePosition(worldPos);
            }
            else
            {
                spearRoot.localPosition = newLocal;
            }

            yield return null;
        }

        // Retroceder
        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            Vector3 newLocal = Vector3.Lerp(targetPos, startPos, t);

            if (spearRigidbody != null)
            {
                Vector3 worldPos = (spearRoot.parent != null) ? spearRoot.parent.TransformPoint(newLocal) : newLocal;
                spearRigidbody.MovePosition(worldPos);
            }
            else
            {
                spearRoot.localPosition = newLocal;
            }

            yield return null;
        }

        // Volver a trigger
        if (spearCollider != null)
            spearCollider.isTrigger = true;

        isAttacking = false;
    }
}