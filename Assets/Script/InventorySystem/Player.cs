using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Item[] itemsToAdd;

    public Inventory myInventory = new Inventory(24);
    private bool isOpen;
    private int selectedHotbarIndex = 0;
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

    public int GetSelectedHotbarIndex()
    {
        return selectedHotbarIndex;
    }
}
