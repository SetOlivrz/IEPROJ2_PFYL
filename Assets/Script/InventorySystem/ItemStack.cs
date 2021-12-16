using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    public Item item;
    public int count;
    public int slotID;

    public ItemStack()
    {
        this.item = null;
        this.count = 0;
        this.slotID = -1;
    }

    public ItemStack(int slotID)
    {
        this.item = null;
        this.count = 0;
        this.slotID = slotID;
    }

    public ItemStack(Item item, int count)
    {
        this.item = item;
        this.count = count;
        this.slotID = -1;
    }

    public ItemStack(Item item, int count, int slotID)
    {
        this.item = item;
        this.count = count;
        this.slotID = slotID;
    }

    public Item GetItem()
    {
        return this.item;
    }

    public int GetCount()
    {
        return count;
    }

    public void setStack(ItemStack stackIn)
    {
        this.item = stackIn.GetItem();
        this.count = stackIn.GetCount();
    }

    public bool IsEmpty()
    {
        return this.count < 1;
    }

    public void IncreaseAmount(int amount)
    {
        this.count += amount;
    }

    public void DecreaseAmount(int amount)
    {
        this.count -= amount;
    }

    public void SetCount(int amount)
    {
        this.count = amount;
    }

    public ItemStack SplitStack(int amount)
    {
        int i = Mathf.Min(amount, count);
        ItemStack copiedStack = this.Copy();
        copiedStack.SetCount(i);
        this.DecreaseAmount(i);
        return copiedStack;
    }

    public ItemStack Copy()
    {
        return new ItemStack(this.item, this.count, this.slotID);
    }

    public bool isItemEqual(ItemStack stackIn)
    {
        return !stackIn.IsEmpty() && this.item == stackIn.GetItem();
    }

    public static bool AreItemsEqual(ItemStack stackA, ItemStack stackB)
    {
        return stackA == stackB ? true : (!stackA.IsEmpty() && !stackB.IsEmpty() ? stackA.isItemEqual(stackB) : false);
    }
}
