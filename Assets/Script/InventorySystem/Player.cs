using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Item[] itemsToAdd;

    public Inventory myInventory = new Inventory(18);
    private bool isOpen;

    private void Start()
    {
        foreach(Item item in itemsToAdd)
        {
            myInventory.AddItem(new ItemStack(item, 1));
        }
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
                InventoryManager.INSTANCE.CloseContainer();
                isOpen = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                InventoryManager.INSTANCE.CloseContainer();
                isOpen = false;
            }
        }
    }
}
