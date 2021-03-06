using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemIcon;
    public Text itemAmount;
    private int slotID;
    private ItemStack myStack;
    private Container attachedContainer;
    private InventoryManager inventoryManager;
    [SerializeField] private GameObject selector;
    [SerializeField] private GameObject[] hotbar;
    private GameObject button;

    public void Start()
    {
        hotbar = GameObject.FindGameObjectsWithTag("Hotbar");

        if(gameObject.tag == "Hotbar")
        {
            button = transform.GetChild(3).gameObject;
            button.SetActive(true);
        }
    }

    public void Update()
    {
        /*if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                foreach (GameObject hotbar in hotbar)
                {
                    hotbar.GetComponent<Slot>().ToggleSelector(false);
                }

                ToggleSelector(true);
            }
        }*/
    }

    public void SetSlot(Inventory attachedInventory, int slotID, Container attachedContainer)
    {
        this.slotID = slotID;
        this.attachedContainer = attachedContainer;
        myStack = attachedInventory.GetStackInSlot(slotID);
        inventoryManager = InventoryManager.INSTANCE;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (!myStack.IsEmpty())
        {
            itemIcon.enabled = true;
            itemIcon.sprite = myStack.GetItem().ItemIcon;

            if(myStack.GetCount() > 1)
            {
                itemAmount.text = myStack.GetCount().ToString();
            }
            else
            {
                itemAmount.text = string.Empty;
            }
        }
        else
        {
            itemIcon.enabled = false;
            itemAmount.text = string.Empty;
        }
    }

    private void SetSlotContents(ItemStack stackIn)
    {
        myStack.SetStack(stackIn);
        UpdateSlot();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ItemStack curDraggedStack = inventoryManager.GetDraggedItemStack();
        ItemStack stackCopy = myStack.Copy();

        if(eventData.pointerId == -1)
        {
            OnLeftClick(curDraggedStack, stackCopy);
        }

        if (eventData.pointerId == -2)
        {
            OnRightClick(curDraggedStack, stackCopy);
        }
    }

    private void SetToolTip(string nameIn)
    {
        inventoryManager.DrawToolTip(nameIn);
    }

    private void OnLeftClick(ItemStack curDraggedStack, ItemStack stackCopy)
    {
        if(!myStack.IsEmpty() && curDraggedStack.IsEmpty())
        {
            inventoryManager.SetDraggedItemStack(stackCopy);
            this.SetSlotContents(ItemStack.Empty);
            SetToolTip(string.Empty);
        }

        if(myStack.IsEmpty() && !curDraggedStack.IsEmpty())
        {
            this.SetSlotContents(curDraggedStack);
            inventoryManager.SetDraggedItemStack(ItemStack.Empty);
            SetToolTip(myStack.GetItem().ItemName);
        }

        if(!myStack.IsEmpty() && !curDraggedStack.IsEmpty())
        {
            if(ItemStack.AreItemsEqual(stackCopy, curDraggedStack))
            {
                if (stackCopy.CanAddToo(curDraggedStack.GetCount()))
                {
                    stackCopy.IncreaseAmount(curDraggedStack.GetCount());
                    this.SetSlotContents(stackCopy);
                    inventoryManager.SetDraggedItemStack(ItemStack.Empty);
                    SetToolTip(myStack.GetItem().ItemName);
                }
                else
                {
                    int difference = (stackCopy.GetCount() + curDraggedStack.GetCount()) - stackCopy.GetItem().maxStackSize;
                    stackCopy.SetCount(myStack.GetItem().maxStackSize);
                    ItemStack dragCopy = curDraggedStack.Copy();
                    dragCopy.SetCount(difference);
                    this.SetSlotContents(stackCopy);
                    inventoryManager.SetDraggedItemStack(dragCopy);
                    SetToolTip(string.Empty);
                }
            }
            else
            {
                ItemStack curDragCopy = curDraggedStack.Copy();
                this.SetSlotContents(curDraggedStack);
                inventoryManager.SetDraggedItemStack(stackCopy);
                SetToolTip(string.Empty);
            }
        }
    }

    private void OnRightClick(ItemStack curDraggedStack, ItemStack stackCopy)
    {
        if(!myStack.IsEmpty() && curDraggedStack.IsEmpty())
        {
            ItemStack stack = stackCopy.SplitStack((stackCopy.GetCount() / 2));
            inventoryManager.SetDraggedItemStack(stack);
            this.SetSlotContents(stackCopy);
            SetToolTip(string.Empty);
        }

        if (myStack.IsEmpty() && !curDraggedStack.IsEmpty())
        {
            this.SetSlotContents(new ItemStack(curDraggedStack.GetItem(), 1));
            ItemStack curDragCopy = curDraggedStack.Copy();
            curDragCopy.DecreaseAmount(1);
            inventoryManager.SetDraggedItemStack(curDragCopy);
            SetToolTip(string.Empty);
        }

        if(!myStack.IsEmpty() && !curDraggedStack.IsEmpty())
        {
            if(ItemStack.AreItemsEqual(stackCopy, curDraggedStack))
            {
                if (myStack.CanAddToo(1))
                {
                    ItemStack newStack = stackCopy;
                    newStack.IncreaseAmount(1);
                    this.SetSlotContents(stackCopy);
                    ItemStack dragCopy = curDraggedStack.Copy();
                    dragCopy.DecreaseAmount(1);
                    inventoryManager.SetDraggedItemStack(dragCopy);
                    SetToolTip(string.Empty);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemStack curDraggedStack = inventoryManager.GetDraggedItemStack();

        if(!myStack.IsEmpty() && curDraggedStack.IsEmpty())
        {
            SetToolTip(myStack.GetItem().ItemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetToolTip(string.Empty);
    }

    public void ToggleSelector(bool active)
    {
        selector.SetActive(active);
    }

    public void Select()
    {
        Debug.Log("Test");
        for(int i = 0; i < hotbar.Length; i++)
        {
            hotbar[i].GetComponent<Slot>().ToggleSelector(false);

            if(hotbar[i] == gameObject)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SetHotbarIndex(i);
            }
        }
    }
}
