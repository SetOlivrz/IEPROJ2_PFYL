using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedDrop : MonoBehaviour
{
    public List<Seed> seedDropList;
    public Seed.SeedTypes seedType;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = seedDropList[(int)seedType].ItemIcon;
    }
}
