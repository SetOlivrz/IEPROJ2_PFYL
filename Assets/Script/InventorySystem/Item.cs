using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Item
{
    public enum ItemType
    {
        Seed,
        Weapon,
    }

    public ItemType itemType;
}
