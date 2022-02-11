using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private float growthSpeed;
    private Seed.SeedTypes type;
    //for future use, plant health
    public int plantHealth { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Seed.SeedTypes GetPlantType()
    {
        return type;
    }

    public void SetPlant(Seed seed)
    {
        this.type = seed.GetSeedType();

        SetGrowthSpeed(seed.GetGrowth());
    }

    public float GetGrowth()
    {
        return growthSpeed;
    }

    public void SetGrowthSpeed(float growthSpeed)
    {
        this.growthSpeed = growthSpeed;
    }
}
