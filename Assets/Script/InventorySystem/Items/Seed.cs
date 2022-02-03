using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed")]
public class Seed : Item
{
    public Seed(SeedTypes type)
    {
        seedType = type;
        maxStackSize = 100;

    }

    public enum SeedTypes
    {
        Rose,
        Cabbage,
        Bush,
    };

    [SerializeField] private SeedTypes seedType;
    [SerializeField] private float growthSpeed;

    public SeedTypes GetSeedType()
    {
        return seedType;
    }

    public float GetGrowth()
    {
        return growthSpeed;
    }
}
