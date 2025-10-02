using UnityEngine;

public class MarketStand : MonoBehaviour
{
    public string itemName; // "Fish", "Meat", "Cake"
    public GameObject itemPrefab; // El prefab visual que se pegará en la lanza
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        TryCollect(other);
    }

    // Nuevo: también revisar colliders que ya están dentro mientras ocurre el ataque
    private void OnTriggerStay(Collider other)
    {
        if (collected) return;
        TryCollect(other);
    }

    // Extrae la lógica de comprobación/adjuntar para reusarla desde Enter/Stay
    private void TryCollect(Collider other)
    {
        // Buscar SpearCollector en el objeto que impactó o en sus padres
        SpearCollector spear = other.GetComponentInParent<SpearCollector>();
        if (spear == null) return;

        // Validar que el impacto ocurrió durante un ataque activo si es posible
        SpearAttack attack = other.GetComponentInParent<SpearAttack>();
        bool validImpact = false;
        if (attack != null)
        {
            validImpact = attack.IsAttacking; // usa la propiedad pública añadida en SpearAttack
        }
        else
        {
            // Fallback: si el collider entrante no es trigger, lo consideramos un impacto válido
            validImpact = !other.isTrigger;
        }

        if (!validImpact) return;

        if (itemPrefab == null)
        {
            Debug.LogWarning("MarketStand: itemPrefab no asignado en " + gameObject.name);
            return;
        }

        bool attached = spear.AttachItem(itemPrefab, itemName);
        if (attached)
        {
            collected = true;
        }
    }
}
