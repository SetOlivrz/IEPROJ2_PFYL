using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    [SerializeField] private List<Item> itemList = new List<Item>();

    private void Start()
    {

    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
}
