using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    public string state = "";
    public bool occupied = false;
    public Plant planted_crop;
    float ticks = 0.0f;

    Sprite seed;

    // Start is called before the first frame update
    void Start()
    {
        seed = Resources.Load<Sprite>("Rose_Seed");
    }

    // Update is called once per frame
    void Update()
    {
        if (occupied)
        {
            if (planted_crop.state != "Third" && this.state == "Watered")
            {
                ticks += Time.deltaTime;
            }

            if (planted_crop.state == "Seed")
            {
                this.transform.GetChild(0).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = seed;
                if (ticks >= planted_crop.growth_interval)
                {
                    this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planted_crop.first_growth;
                    planted_crop.state = "First";
                    ticks = 0.0f;
                }
            }

            else if (planted_crop.state == "First")
            {
                this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planted_crop.first_growth;
                if (ticks >= planted_crop.growth_interval)
                {
                    this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planted_crop.second_growth;
                    planted_crop.state = "Second";
                    ticks = 0.0f;
                }
            }

            else if (planted_crop.state == "Second")
            {
                this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planted_crop.second_growth;
                if (ticks >= planted_crop.growth_interval)
                {
                    this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planted_crop.third_growth;
                    planted_crop.state = "Third";
                    ticks = 0.0f;
                }
            }
        }
    }
}
