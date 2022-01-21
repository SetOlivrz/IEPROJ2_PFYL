using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggedItemStack : MonoBehaviour
{
    public Image itemIcon;
    public Text itemAmount;

    private ItemStack myStack = ItemStack.Empty;

    public void SetDraggedStack(ItemStack stackIn)
    {
        myStack = stackIn;
    }

    private void drawStack()
    {
        if (!myStack.IsEmpty())
        {
            itemIcon.enabled = true;
            itemIcon.sprite = myStack.GetItem().ItemIcon;

            if (myStack.GetCount() > 1)
            {
                itemAmount.text = myStack.GetCount().ToString();
            }
            else
            {
                itemAmount.text = string.Empty;
            }
        }
        else
        {
            DisableDragStack();
        }
    }

    private void DisableDragStack()
    {
        itemIcon.enabled = false;
        itemAmount.text = string.Empty;
    }

    private void Update()
    {
        drawStack();
        transform.position = Input.mousePosition;
    }
}
