using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Item
{
    public enum SeedType
    {
        Rose,
        Bomb
    };

    public SeedType seedType;
    public float growthRate;

    Seed()
    {
        itemType = ItemType.Seed;

        switch (seedType)
        {
            case (SeedType.Rose):
                //change attributes here accordingly (not final value)
                growthRate = 5;
                break;
            case (SeedType.Bomb):
                growthRate = 10;
                break;
        }
    }
}
