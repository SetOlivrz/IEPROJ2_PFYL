using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanting : MonoBehaviour
{
    [SerializeField] Player player;
    private Soil soil;

    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            player.myInventory.AddItem(new ItemStack(player.myInventory.itemsToAdd[0], 1));
            InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, player.myInventory));
        }*/

        if (Input.GetMouseButtonDown(0) && !player.isOpen)
        {
            Debug.Log("Mouse Clicked!");

            ItemStack currentHeldItem = player.myInventory.GetInventoryStacks()[player.GetSelectedHotbarIndex()];

            if (currentHeldItem.GetItem() != null)
                Debug.Log(currentHeldItem.GetItem().ItemName);
            else
                Debug.Log("Hands");

            // Checks if player can plant when left clicking
            if (soil)
            {
                if (currentHeldItem.GetItem() is Tool tool)
                {
                    switch (tool.GetToolType())
                    {
                        case Tool.ToolTypes.Hoe:
                            soil.Till();
                            break;
                        case Tool.ToolTypes.WateringCan:
                            if(soil.GetHasSeed() && !soil.isGrowing)
                            soil.Water();
                            break;
                    }
                }

                if(currentHeldItem.GetItem() == null && soil.GetIsGrown())
                {
                    //Check if inventory is full
                    if(player.myInventory.GetInventorySize() > player.myInventory.GetItemsCount())
                    {
                        foreach(Item itemsToAdd in soil.seedDrops[(int)soil.plant.GetPlantType()].items)
                        {
                            player.myInventory.AddItem(new ItemStack(itemsToAdd, 1));
                        }

                        //player.myInventory.AddItem(drops);
                        InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, player.myInventory));
                    }
                    else
                    {
                        //If inventory is full
                            //drop item(s)
                    }

                    soil.Harvest();
                }

                if (currentHeldItem.GetItem() is Seed seed && soil.GetIsTilled() && !soil.GetHasSeed())
                {
                    //get seed from inventory and remove 1 instance
                    currentHeldItem.DecreaseAmount(1);
                    if(currentHeldItem.GetCount() < 1)
                    {
                        currentHeldItem.RemoveItem();
                    }

                    InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, player.myInventory));

                    soil.Plant(seed);
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Soil")
        {
            soil = other.gameObject.GetComponent<Soil>();
        }

        Debug.Log("In range!");
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Soil")
        {
            if(other.gameObject == soil.gameObject)
            {
                soil = null;
            }
        }

        Debug.Log("Out of range!");
    }
}
