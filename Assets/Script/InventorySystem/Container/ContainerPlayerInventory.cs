using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPlayerInventory : Container
{
    public ContainerPlayerInventory(Inventory containerInventory, Inventory playerInventory) : base (containerInventory, playerInventory)
    {
        for (int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, i, 55 + (50 * i), -55, 50);
        }
        for (int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, 6 + i, 55 + (50 * i), -105, 50);
        }
        for (int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, 12 + i, 55 + (50 * i), -155, 50);
        }
    }

    public override GameObject GetContainerPrefab()
    {
        return InventoryManager.INSTANCE.GetContainerPrefab("Player Inventory");
    }
}
