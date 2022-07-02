using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPlayerInventory : Container
{
    public ContainerPlayerInventory(Inventory containerInventory, Inventory playerInventory) : base (containerInventory, playerInventory)
    {
        
        for(int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, i, 1); // hotbarslots inside the player inventory // default offset = -125
        }

        for (int i = 6; i < 24; i++)
        {
            AddSlotToContainer(playerInventory, i, 0);// for container
        }
    }

    public override GameObject GetContainerPrefab()
    {
        return InventoryManager.INSTANCE.GetContainerPrefab("PlayerInventory");
    }
}
