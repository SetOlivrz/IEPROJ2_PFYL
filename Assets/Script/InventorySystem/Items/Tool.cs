using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool")]
public class Tool : Item
{
    public enum ToolTypes
    {
        Hoe,
        WateringCan,
        RoseSword
    };

    [SerializeField] private ToolTypes toolType;

    public ToolTypes GetToolType()
    {
        return toolType;
    }
}
