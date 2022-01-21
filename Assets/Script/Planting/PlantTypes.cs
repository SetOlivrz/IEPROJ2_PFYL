using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTypes : MonoBehaviour
{
    public static Plant Rose()
    {
        Plant rose;
        rose.name = "Rose";
        rose.growth_interval = 5.0f;
        rose.state = "Seed";
        rose.first_growth = Resources.Load<Sprite>("Rose_Seed");
        rose.second_growth = Resources.Load<Sprite>("Rose_Growing");
        rose.third_growth = Resources.Load<Sprite>("Rose_Dagger");

        return rose;
    }

    //public static Plant Turnip()
    //{
    //    Plant turnip;
    //    turnip.name = "Turnip";
    //    turnip.growth_interval = 2.0f;
    //    turnip.state = "Seed";
    //    turnip.first_growth = Resources.Load<Sprite>("first_growth");
    //    turnip.second_growth = Resources.Load<Sprite>("second_growth");
    //    turnip.third_growth = Resources.Load<Sprite>("third_growth");

    //    return turnip;
    //}
}
