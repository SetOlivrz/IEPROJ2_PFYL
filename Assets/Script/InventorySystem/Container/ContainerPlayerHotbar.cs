using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPlayerHotbar : Container
{
    public ContainerPlayerHotbar(Inventory containerInventory, Inventory playerInventory) : base(containerInventory, playerInventory)
    {
        for(int i = 0; i < 6; i++)
        {
            AddHotbarSlotToContainer(playerInventory, i, 0);
        }
    }

    public override GameObject GetContainerPrefab()
    {
        return InventoryManager.INSTANCE.GetContainerPrefab("PlayerHotbar");
    }
}
