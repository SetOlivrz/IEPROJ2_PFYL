using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : MonoBehaviour
{
    public float range = 15f;
    public enum Weapon
    {
        Range,
        Melee
    }

    public Weapon type;

    // Start is called before the first frame update
    void Start()
    {
        //currently setting the range or reach of the weapon
        switch (type)
        {
            case Weapon.Range: range = 10f; break;
            case Weapon.Melee: range = 5f; break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
