using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager INSTANCE;

    private void Awake()
    {
        INSTANCE = this;
    }
    #endregion

    public GameObject slotPrefab;

    public List<ContainerGetter> containers = new List<ContainerGetter>();
    private Container currentOpenContainer;
    private ItemStack curDraggedStack = ItemStack.Empty;
    private GameObject spawnedDragStack;
    private DraggedItemStack dragStack;
    private Tooltip tooltip;

    private void Start()
    {
        dragStack = GetComponentInChildren<DraggedItemStack>();
        tooltip = GetComponentInChildren<Tooltip>();
    }

    public GameObject GetContainerPrefab(string name)
    {
        foreach(ContainerGetter container in containers)
        {
            if(container.containerName == name)
            {
                return container.containerpPrefab;
            }
        }
        return null;
    }

    public void OpenContainer(Container container)
    {
        if(currentOpenContainer != null)
        {
            currentOpenContainer.CloseContainer();
        }

        currentOpenContainer = container;
    }

    public void CloseContainer()
    {
        if (currentOpenContainer != null)
        {
            currentOpenContainer.CloseContainer();
        }
    }

    public ItemStack GetDraggedItemStack()
    {
        return curDraggedStack;
    }

    public void SetDraggedItemStack(ItemStack stackIn)
    {
        dragStack.SetDraggedStack(curDraggedStack = stackIn);
    }

    public void DrawToolTip(string itemName)
    {
        tooltip.SetToolTip(itemName);
    }
}

[System.Serializable]
public class ContainerGetter
{
    public string containerName;
    public GameObject containerpPrefab;
}
