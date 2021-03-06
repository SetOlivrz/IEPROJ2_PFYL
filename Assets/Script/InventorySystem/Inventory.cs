using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] private List<ItemStack> inventoryContents = new List<ItemStack>();

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
            /*if (stack.IsEmpty())
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
            }*/

            if (ItemStack.AreItemsEqual(input, stack))
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

        foreach(ItemStack stack in inventoryContents)
        {
            if (stack.IsEmpty())
            {
                stack.SetStack(input);
                return true;
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

    public int GetInventorySize()
    {
        return inventoryContents.Count;
    }

    public int GetItemsCount()
    {
        int count = 0;
        foreach(ItemStack items in inventoryContents)
        {
            if(items.GetItem() != null)
            {
                count++;
            }
        }

        return count;
    }
}
