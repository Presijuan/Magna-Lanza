using UnityEngine;

public class SpearCollector : MonoBehaviour
{
    // Tres posibles puntos de anclaje para objetos en la lanza
    public Transform attachPoint1;
    public Transform attachPoint2;
    public Transform attachPoint3;

    // Intenta adjuntar el item al primer attachPoint vacío. Devuelve true si tuvo éxito.
    public bool AttachItem(GameObject prefab, string itemName)
    {
        Transform[] slots = new Transform[] { attachPoint1, attachPoint2, attachPoint3 };

        for (int i = 0; i < slots.Length; i++)
        {
            Transform slot = slots[i];
            if (slot == null)
            {
                Debug.LogWarning($"SpearCollector: attachPoint{i + 1} no está asignado.");
                continue;
            }

            // Consideramos el slot vacío si no tiene hijos
            if (slot.childCount == 0)
            {
                GameObject newItem = Instantiate(prefab, slot.position, slot.rotation, slot);
                newItem.name = itemName;

                // Asegurar posición/rotación local correcta
                newItem.transform.localPosition = Vector3.zero;
                newItem.transform.localRotation = Quaternion.identity;

                // Si el item tiene Rigidbody, desactivar la física para que siga a la lanza
                Rigidbody rb = newItem.GetComponent<Rigidbody>() ?? newItem.GetComponentInChildren<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.linearVelocity = Vector3.zero; // corregido
                    rb.angularVelocity = Vector3.zero;
                }

                // Hacer colliders como trigger para evitar interferencias
                Collider[] cols = newItem.GetComponentsInChildren<Collider>();
                foreach (var c in cols)
                {
                    c.isTrigger = true;
                }

                // Marca la lista de compras
                FindObjectOfType<MarketListManager>()?.CollectItem(itemName);

                return true;
            }
        }

        // No encontró slot vacío
        return false;
    }
}
