using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPlayerInventory : Container
{
    public ContainerPlayerInventory(Inventory containerInventory, Inventory playerInventory) : base (containerInventory, playerInventory)
    {
        
        for(int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, i, -137 + (55 * i), -90, 50); // hotbarslots inside the player inventory // default offset = -125
        }

        for (int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, 6 + i, -137 + (55 * i), 95, 50);// adjustment : 90 ->-95
        }
        for (int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, 12 + i, -137 + (55 * i), 40, 50);
        }
        for (int i = 0; i < 6; i++)
        {
            AddSlotToContainer(playerInventory, 18 + i, -137 + (55 * i), -15, 50); // adjustment : -10 ->-15
        }
    }

    public override GameObject GetContainerPrefab()
    {
        return InventoryManager.INSTANCE.GetContainerPrefab("PlayerInventory");
    }
}
