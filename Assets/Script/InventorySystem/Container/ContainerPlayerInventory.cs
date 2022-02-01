using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPlayerInventory : Container
{
    public ContainerPlayerInventory(Inventory containerInventory, Inventory playerInventory) : base (containerInventory, playerInventory)
    {
        for(int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, i, -125 + (50 * i), -90, 50);
        }

        for (int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, 6 + i, -125 + (50 * i), 90, 50);
        }
        for (int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, 12 + i, -125 + (50 * i), 40, 50);
        }
        for (int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, 18 + i, -125 + (50 * i), -10, 50);
        }
    }

    public override GameObject GetContainerPrefab()
    {
        return InventoryManager.INSTANCE.GetContainerPrefab("PlayerInventory");
    }
}
