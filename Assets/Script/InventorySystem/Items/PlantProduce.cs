using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Produce")]
public class PlantProduce : Item
{
    public enum ProduceTypes
    {
        RoseSword,
        CabbageBomb,
        Bush
    };

    [SerializeField] private ProduceTypes produceTypes;

    public ProduceTypes GetProduceType()
    {
        return produceTypes;
    }
}
