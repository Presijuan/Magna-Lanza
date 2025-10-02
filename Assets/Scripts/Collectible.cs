using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string itemName; // Asigna: "Spear", "Fish", "Meat", "Cake"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var mgr = FindObjectOfType<MarketListManager>();
            if (mgr != null)
            {
                mgr.CollectItem(itemName);
            }
            else
            {
                Debug.LogWarning("Collectible: MarketListManager no encontrado al recoger " + itemName);
            }
        }
    }
}
