using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanting : MonoBehaviour
{
    private Player player;
    private Soil soil;

    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked!");

            ItemStack currentHeldItem = player.myInventory.GetInventoryStacks()[player.GetSelectedHotbarIndex()];

            Debug.Log(currentHeldItem.GetItem().ItemName);

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
                            if(soil.GetHasSeed())
                            soil.Water();
                            break;
                    }
                }

                if (currentHeldItem.GetItem() is Seed seed && soil.GetIsTilled() && !soil.GetHasSeed())
                {
                    //get seed from inventory and remove 1 instance
                    currentHeldItem.DecreaseAmount(1);
                    InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, player.myInventory));

                    soil.SetHasSeed(true);
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
            soil = null;
        }

        Debug.Log("Out of range!");
    }
}
