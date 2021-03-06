using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlanting : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TutorialActionManager manager;
    private Soil soil;
    bool check = false;

    void Start()
    {
        player = gameObject.GetComponent<Player>();

        if (manager == null)
        {
            check = false;
        }
        else
        {
            check = true;
        }

    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            player.myInventory.AddItem(new ItemStack(player.myInventory.itemsToAdd[0], 1));
            InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, player.myInventory));
        }*/

        if (check == true)
        {
            if (manager != null&& manager.currentStep==30)
            {
                manager.hasFullyGrown = soil.GetIsGrown();
            }
        }
        

        if (Input.GetMouseButton(1))
        {
            if (soil)
            {
                if (soil.GetIsGrown())
                {
                    foreach (Item itemsToAdd in soil.seedDrops[(int)soil.plant.GetPlantType()].items)
                    {
                        if (itemsToAdd is Seed x)
                        {
                            GameObject itemDrop = GameObject.Instantiate(soil.seedDrop, gameObject.transform);
                            SeedDrop seedDrop = itemDrop.GetComponent<SeedDrop>();

                            seedDrop.seedType = x.GetSeedType();
                            itemDrop.GetComponent<SpriteRenderer>().sprite = seedDrop.seedDropList[(int)seedDrop.seedType].ItemIcon;
                        }
                        if (itemsToAdd is PlantProduce y)
                        {
                            GameObject itemDrop = GameObject.Instantiate(soil.produceDrop, gameObject.transform);
                            PlantProduceDrop produceDrop = itemDrop.GetComponent<PlantProduceDrop>();

                            produceDrop.produceType = y.GetProduceType();
                            itemDrop.GetComponent<SpriteRenderer>().sprite = produceDrop.plantProduceList[(int)produceDrop.produceType].ItemIcon;
                        }
                    }

                    soil.Harvest();
                }
            }
        }

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
                            if(!soil.GetIsTilled())
                            {
                                // tutorial
                                if (manager != null) // in tutorial
                                {
                                    if (manager.currentStep == 15 || manager.currentStep >= 30)
                                    {
                                        manager.hasUsedHoe = true;
                                        soil.Till();
                                    }
                                }
                                else // not in tutorial
                                {
                                    soil.Till();
                                }
                            }
                            break;
                        case Tool.ToolTypes.WateringCan:
                            if(soil.GetHasSeed() && !soil.isGrowing)
                                if(soil.GetHasSeed())
                                {
                                    soil.Water();

                                    // tutorial
                                    if (manager != null)
                                    {
                                        manager.hasWateredPlant = true;
                                    }
                                }
                                   
                            break;
                    }
                }

                if(currentHeldItem.GetItem() == null && soil.GetIsGrown())
                {
                    //Check if inventory is full
                    if(player.myInventory.GetInventorySize() > player.myInventory.GetItemsCount())
                    {
                        /*foreach (Item itemsToAdd in soil.seedDrops[(int)soil.plant.GetPlantType()].items)
                        {
                            player.myInventory.AddItem(new ItemStack(itemsToAdd, 1));
                        }*/

                        //GameObject.Instantiate(soil.seedDrop, gameObject.transform.parent);

                        foreach (Item itemsToAdd in soil.seedDrops[(int)soil.plant.GetPlantType()].items)
                        {
                            if(itemsToAdd is Seed x)
                            {
                                GameObject itemDrop = GameObject.Instantiate(soil.seedDrop, gameObject.transform);
                                SeedDrop seedDrop = itemDrop.GetComponent<SeedDrop>();

                                seedDrop.seedType = x.GetSeedType();
                                itemDrop.GetComponent<SpriteRenderer>().sprite = seedDrop.seedDropList[(int)seedDrop.seedType].ItemIcon;
                            }
                            if(itemsToAdd is PlantProduce y)
                            {
                                GameObject itemDrop = GameObject.Instantiate(soil.produceDrop, gameObject.transform);
                                PlantProduceDrop produceDrop = itemDrop.GetComponent<PlantProduceDrop>();

                                produceDrop.produceType = y.GetProduceType();
                                itemDrop.GetComponent<SpriteRenderer>().sprite = produceDrop.plantProduceList[(int)produceDrop.produceType].ItemIcon;
                            }
                        }

                        InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, player.myInventory));
                    }
                    else
                    {
                        //If inventory is full
                            //drop item(s)
                    }

                    soil.Harvest();

                    // tutorial
                   

                    if (check == true)
                    {
                        if (manager != null)
                        {
                            manager.hasHarvested = true;
                        }
                    }
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

 
                    if (check == true)
                    {
                        if (manager != null)
                        {
                            manager.hasPlantedSeed = true;
                        }
                    }
                    soil.Plant(seed);
                    // tutorial
                   
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
        if (soil == null) return;

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
