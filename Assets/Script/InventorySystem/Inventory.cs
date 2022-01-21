using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<ItemStack> inventoryContents = new List<ItemStack>();

    public Inventory(int size)
    {
        for(int i = 0; i < size; i++)
        {
            inventoryContents.Add(new ItemStack(i));
        }
    }

    public bool AddItem(ItemStack input)
    {
        foreach(ItemStack stack in inventoryContents)
        {
            if (stack.IsEmpty())
            {
                stack.SetStack(input);
                return true;
            }
            else
            {
                if(ItemStack.AreItemsEqual(input, stack))
                {
                    if (stack.CanAddToo(input.GetCount()))
                    {
                        stack.IncreaseAmount(input.GetCount());
                        return true;
                    }
                    else
                    {
                        int difference = (stack.GetCount() + input.GetCount() - stack.GetItem().maxStackSize);
                        stack.SetCount(stack.GetItem().maxStackSize);
                        input.SetCount(difference);
                    }
                }
            }
        }
        return false;
    }

    public ItemStack GetStackInSlot(int index)
    {
        return inventoryContents[index];
    }

    public List<ItemStack> GetInventoryStacks()
    {
        return inventoryContents;
    }
}
