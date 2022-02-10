using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantProduceDrop : MonoBehaviour
{
    public List<PlantProduce> plantProduceList;
    public PlantProduce.ProduceTypes produceType;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = plantProduceList[(int)produceType].ItemIcon;
    }
}
