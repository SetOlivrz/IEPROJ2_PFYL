using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    //enum should be here I guess for plant types
    public enum PlantType
    {
        Rose,
        Bomb
    };

    public float growthSpeed;
    public PlantType type;

    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case PlantType.Rose:
                growthSpeed = 5f;
                break;
            case PlantType.Bomb:
                growthSpeed = 5.5f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
