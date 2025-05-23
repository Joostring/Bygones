using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject currentItem;
    public GameObject hoverBox;       
    public TMP_Text hoverText;        

    public string defaultText = "";

    private void Start()
    {
        hoverBox.SetActive(false); 
        hoverText.text = "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            hoverBox.SetActive(true);

            
            string infoText = GetHoverText(currentItem.name);
            hoverText.text = infoText;
        }
        else
        {
            hoverBox.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverText.text = ""; 
        hoverBox.SetActive(false); 
    }

    private string GetHoverText(string itemName)
    {
        switch (itemName)
        {
            case "Key_Decorative_Inspect": return "Decorative Key" ;
            case "Key_Gen_Inspect": return "Generator key";
            case "Key_Office_Inspect": return "Office key" ;
            case "Key_Front_Inspect": return "Front door key" ;
            case "Flashlight_Inspect": return "Flashlight" ;
            case "Note_1_Inspect": return "Lawyer's Note";
            case "Key_Gate_Inspect": return "Gate key";
            case "Matches_Inspect": return "Matchbox";
            case "Newspaper_Inspect": return "Newspaper article";
            case "Key_Basement_Inspect": return "Big old key";
            case "Note_2_Inspect": return "Divorce papers";
            case "Note_3_Inspect": return "A note from a mother";
            case "Note_4_Inspect": return "A letter from Martha";
            case "Note_Diary_Inspect": return "Ollies diary";
            case "Note_Final_Inspect": return "Note from my mother";
            default: return null;;
        }
    }
}
