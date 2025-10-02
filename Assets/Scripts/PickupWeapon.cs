using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public Transform handSocket;
    private bool isEquipped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isEquipped)
        {
            Equip(other.transform);
        }
    }

    void Equip(Transform player)
    {
        isEquipped = true;
        transform.SetParent(handSocket);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero; // corregido
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("PickupWeapon: No se encontró Rigidbody en la lanza al equipar.");
        }

        // Marcar la lanza equipada para que los MarketStand la reconozcan
        gameObject.tag = "Spear";

        // Asegurar que los colliders de la lanza sean triggers mientras está equipada (evita física indeseada)
        Collider[] cols = GetComponentsInChildren<Collider>();
        foreach (var c in cols)
        {
            c.isTrigger = true;
        }

        // Activar el script SpearAttack si existe (normalmente estará desactivado al inicio)
        SpearAttack attack = GetComponentInChildren<SpearAttack>(true);
        if (attack != null)
        {
            attack.enabled = true;
        }
        else
        {
            Debug.LogWarning("PickupWeapon: No se encontró componente SpearAttack en la lanza.");
        }
    }
}