using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slots = 5;

    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemUsed;

    internal void UseItem(IInventoryItem item)
    {
        if (ItemUsed != null)
        {
            ItemUsed(this, new InventoryEventArgs(item));
        }
    }

/*    public Inventory()
    {
        for (int i = 0; i < slots; i++) {
            mItems.Add(new InventorySlot(i));
        }
    }*/

/*    private InventorySlot FindStackableSlot(IInventoryItem item)
    {
        foreach(InventorySlot slot in mItems)
        {
            if (slot.IsStackable(item))
            {
                return slot;
            }
            return null;
        }
    }*/

/*    private InventorySlot FindNextEmptySlot()
    {
        foreach(InventorySlot slot in mItems)
        {
            if (slot.IsEmpty)
            {
                return slot;
            }
            return null;
        }
    }*/

    public void AddItem(IInventoryItem item)
    {
        if(mItems.Count < slots)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.onPickup();

                if(ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            Debug.Log("Dropping");
            mItems.Remove(item);
            item.onDrop();

            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if(collider != null)
            {
                collider.enabled = true;
            }
            if(ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
}
