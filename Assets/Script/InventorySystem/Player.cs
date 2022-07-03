using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Item[] itemsToAdd;

    public Inventory myInventory = new Inventory(24);
    public bool isOpen { get; private set; }
    private int selectedHotbarIndex = 0;
    [SerializeField] private GameObject toolTip;
    [SerializeField] private bool mobileMode = false;
    public bool MobileMode { get { return mobileMode; } }

    private void Start()
    {
        foreach(Item item in itemsToAdd)
        {
            myInventory.AddItem(new ItemStack(item, 1));
        }

        InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, myInventory));
        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isOpen)
            {
                InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerInventory(null, myInventory));
                isOpen = true;
            }
            else
            {
                InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, myInventory));
                isOpen = false;
                toolTip.GetComponent<Image>().enabled = false;
                toolTip.transform.GetChild(0).GetComponent<Text>().text = "";
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, myInventory));
                isOpen = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedHotbarIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            selectedHotbarIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            selectedHotbarIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            selectedHotbarIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            selectedHotbarIndex = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            selectedHotbarIndex = 5;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            selectedHotbarIndex = 6;


        /*if (Input.GetKeyDown(KeyCode.E))
        {
            myInventory.AddItem(new ItemStack(itemsToAdd[2], 1));
            InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, myInventory));
        }*/

        UpdateSelectedHotbarIndex(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void UpdateSelectedHotbarIndex(float direction)
    {
        if (direction > 0)
            direction = 1;
        if (direction < 0)
            direction = -1;

        for (selectedHotbarIndex -= (int)direction; selectedHotbarIndex < 0; selectedHotbarIndex += 6);

        while (selectedHotbarIndex >= 6)
            selectedHotbarIndex -= 6;
    }

    public void SetHotbarIndex(int index)
    {
        selectedHotbarIndex = index;
    }

    public int GetSelectedHotbarIndex()
    {
        return selectedHotbarIndex;
    }
    public void OpenInventory()
    {
        if (!isOpen)
        {
            InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerInventory(null, myInventory));
            isOpen = true;
        }
        else
        {
            InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, myInventory));
            isOpen = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SeedDrop")
        {
            SeedDrop drop = collision.gameObject.GetComponent<SeedDrop>();
            myInventory.AddItem(new ItemStack(drop.seedDropList[(int)drop.seedType], 1));
            InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, myInventory));

            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "PlantProduceDrop")
        {
            PlantProduceDrop drop = collision.gameObject.GetComponent<PlantProduceDrop>();
            myInventory.AddItem(new ItemStack(drop.plantProduceList[(int)drop.produceType], 1));
            InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, myInventory));

            Destroy(collision.gameObject);
        }
    }
}
