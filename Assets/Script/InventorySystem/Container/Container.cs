using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container
{
    private List<Slot> slots = new List<Slot>();
    private GameObject spawnedContainerPrefab;
    private Inventory containerInventory;
    private Inventory playerInventory;

    public Container(Inventory containerInventory, Inventory playerInventory)
    {
        this.containerInventory = containerInventory;
        this.playerInventory = playerInventory;
        OpenContainer();
    }

    public void AddSlotToContainer(Inventory inventory, int slotID, int containerIndex)
    {
        GameObject spawnedSlot = Object.Instantiate(InventoryManager.INSTANCE.slotPrefab);
        Slot slot = spawnedSlot.GetComponent<Slot>();
        slot.SetSlot(inventory, slotID, this);
        spawnedSlot.transform.SetParent(spawnedContainerPrefab.transform.GetChild(containerIndex));
        spawnedSlot.transform.SetAsFirstSibling();
        slots.Add(slot);
    }

    public void UpdateSlots()
    {
        foreach(Slot slot in slots)
        {
            slot.UpdateSlot();
        }
    }

    public void OpenContainer()
    {
        spawnedContainerPrefab = Object.Instantiate(GetContainerPrefab(), InventoryManager.INSTANCE.transform);
        spawnedContainerPrefab.transform.SetAsFirstSibling();
    }

    public void CloseContainer()
    {
        Object.Destroy(spawnedContainerPrefab);
    }

    //Needs to be overridden, can not be left blank or null
    public virtual GameObject GetContainerPrefab()
    {
        return null;
    }

    public GameObject GetSpawnedContainer()
    {
        return spawnedContainerPrefab;
    }

    public Inventory GetContainerInventory()
    {
        return containerInventory;
    }

    public Inventory GetPlayerInventory()
    {
        return playerInventory;
    }
}
