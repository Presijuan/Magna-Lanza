using UnityEngine;
using TMPro;

public class MarketListManager : MonoBehaviour
{
    public TextMeshProUGUI spearText;
    public TextMeshProUGUI fishText;
    public TextMeshProUGUI meatText;
    public TextMeshProUGUI cakeText;

    public void CollectItem(string item)
    {
        switch(item)
        {
            case "Spear":
                spearText.text = "   ✔ Arma";
                spearText.fontStyle = FontStyles.Strikethrough | FontStyles.Italic;
                break;
            case "Fish":
                fishText.text = "   ✔ Pescado";
                fishText.fontStyle = FontStyles.Strikethrough | FontStyles.Italic;
                break;
            case "Meat":
                meatText.text = "   ✔ Carne";
                meatText.fontStyle = FontStyles.Strikethrough | FontStyles.Italic;
                break;
            case "Cake":
                cakeText.text = "   ✔ Pastel";
                cakeText.fontStyle = FontStyles.Strikethrough | FontStyles.Italic;
                break;
        }
    }
}