using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory inventory;
    //public GameObject message;
    // Start is called before the first frame update
    void Start()
    {
        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += InventoryScript_ItemRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Hotbar");
        foreach(Transform slot in inventoryPanel)
        {
            // for when the border is created
            /*Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();*/
            Transform imageTransform = slot.GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                //Stores a reference to the item
                Debug.Log(e.Item);
                itemDragHandler.Item = e.Item;
                break;
            }
        }
    }

    private void InventoryScript_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Hotbar");
        foreach (Transform slot in inventoryPanel)
        {
            // for when the border is created
            Transform imageTransform = slot.GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if (itemDragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDragHandler.Item = null;
                break;
            }
        }
    }

/*    public void OpenMessagePanel(string text)
    {
        message.SetActive(true);
    }
    public void CloseMessagePanel(string text)
    {
        message.SetActive(false);
    }*/
}
