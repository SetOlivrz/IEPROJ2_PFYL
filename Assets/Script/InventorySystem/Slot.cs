using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler
{
    public Image itemIcon;
    public Text itemAmount;
    private int slotID;
    private ItemStack myStack;
    private Container attachedContainer;
    private InventoryManager inventoryManager;

    public void SetSlot(Inventory attachedInventory, int slotID, Container attachedContainer)
    {
        this.slotID = slotID;
        this.attachedContainer = attachedContainer;
        myStack = attachedInventory.GetStackInSlot(slotID);
        inventoryManager = InventoryManager.INSTANCE;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (!myStack.IsEmpty())
        {
            itemIcon.enabled = true;
            itemIcon.sprite = myStack.GetItem().ItemIcon;

            if(myStack.GetCount() > 1)
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
            itemIcon.enabled = false;
            itemAmount.text = string.Empty;
        }
    }

    private void SetSlotContents(ItemStack stackIn)
    {
        myStack.SetStack(stackIn);
        UpdateSlot();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ItemStack curDraggedStack = inventoryManager.GetDraggedItemStack();
        ItemStack stackCopy = myStack.Copy();

        if(eventData.pointerId == -1)
        {
            OnLeftClick(curDraggedStack, stackCopy);
        }

        if (eventData.pointerId == -2)
        {
            OnRightClick(curDraggedStack, stackCopy);
        }
    }

    private void OnLeftClick(ItemStack curDraggedStack, ItemStack stackCopy)
    {
        if(!myStack.IsEmpty() && curDraggedStack.IsEmpty())
        {
            inventoryManager.SetDraggedItemStack(stackCopy);
            this.SetSlotContents(ItemStack.Empty);
        }

        if(myStack.IsEmpty() && !curDraggedStack.IsEmpty())
        {
            this.SetSlotContents(curDraggedStack);
            inventoryManager.SetDraggedItemStack(ItemStack.Empty);
        }

        if(!myStack.IsEmpty() && !curDraggedStack.IsEmpty())
        {
            if(ItemStack.AreItemsEqual(stackCopy, curDraggedStack))
            {
                if (stackCopy.CanAddToo(curDraggedStack.GetCount()))
                {
                    stackCopy.IncreaseAmount(curDraggedStack.GetCount());
                    this.SetSlotContents(stackCopy);
                    inventoryManager.SetDraggedItemStack(ItemStack.Empty);
                }
                else
                {
                    int difference = (stackCopy.GetCount() + curDraggedStack.GetCount()) - stackCopy.GetItem().maxStackSize;
                    stackCopy.SetCount(myStack.GetItem().maxStackSize);
                    ItemStack dragCopy = curDraggedStack.Copy();
                    dragCopy.SetCount(difference);
                    this.SetSlotContents(stackCopy);
                    inventoryManager.SetDraggedItemStack(dragCopy);
                }
            }
            else
            {
                ItemStack curDragCopy = curDraggedStack.Copy();
                this.SetSlotContents(curDraggedStack);
                inventoryManager.SetDraggedItemStack(stackCopy);
            }
        }
    }

    private void OnRightClick(ItemStack curDraggedStack, ItemStack stackCopy)
    {
        if(!myStack.IsEmpty() && curDraggedStack.IsEmpty())
        {
            ItemStack stack = stackCopy.SplitStack((stackCopy.GetCount() / 2));
            inventoryManager.SetDraggedItemStack(stack);
            this.SetSlotContents(stackCopy);
        }

        if (myStack.IsEmpty() && !curDraggedStack.IsEmpty())
        {
            this.SetSlotContents(new ItemStack(curDraggedStack.GetItem(), 1));
            ItemStack curDragCopy = curDraggedStack.Copy();
            curDragCopy.DecreaseAmount(1);
            inventoryManager.SetDraggedItemStack(curDragCopy);
        }

        if(!myStack.IsEmpty() && !curDraggedStack.IsEmpty())
        {
            if(ItemStack.AreItemsEqual(stackCopy, curDraggedStack))
            {
                if (myStack.CanAddToo(1))
                {
                    ItemStack newStack = stackCopy;
                    newStack.IncreaseAmount(1);
                    this.SetSlotContents(stackCopy);
                    ItemStack dragCopy = curDraggedStack.Copy();
                    dragCopy.DecreaseAmount(1);
                    inventoryManager.SetDraggedItemStack(dragCopy);
                }
            }
        }
    }
}
