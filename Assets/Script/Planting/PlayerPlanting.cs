using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerPlanting : MonoBehaviour
{
    [SerializeField] PlayerController player;
    private GameObject soil;
    private GameObject prev_soil;
    [SerializeField] private string equipped_tool;
    [SerializeField] GameObject equipped_sprite;
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject leftHand;
    bool inContact = false;

    // r g b a
    Color tilled_soil_color = new Color((float)160 / 255, (float)132 / 255, (float)107 / 255, (float)110 / 255);
    Color planted_soil_color = new Color((float)155 / 255, (float)132 / 255, (float)107 / 255, (float)55 / 255);
    Color watered_soil_color = new Color((float)61 / 255, (float)38 / 255, (float)26 / 255, (float)225 / 255);
    Color empty_soil_color = new Color((float)212 / 255, (float)212 / 255, (float)212 / 255, (float)225 / 255);

    // Start is called before the first frame update
    void Start()
    {
        equipped_tool = "hand";
        equipped_sprite.SetActive(false);
        rightHand.SetActive(false);
        leftHand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //CheckPlayerTouch();

        ChangeEquipment();

        if (Input.GetMouseButtonDown(0))
        {
            switch (equipped_tool)
            {
                case "hoe": UseHoe(); player.isShooting = false;  break;

                case "seed": UseSeed(); player.isShooting = false; break;

                case "watering_can":  UseWateringCan(); player.isShooting = false; break;

                case "hand": player.isShooting = false; UseHand(); break;
            }            
        }
    }

    private bool CheckPlayerTouch()
    {
        if (Vector3.Distance(gameObject.transform.position, soil.transform.localPosition) <= 1.0f)
        {
            Debug.Log("Soil");
            return true;
        }

        return false;
    }
    //Temp holder for switching
    void ChangeEquipment()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equipped_tool = "hoe";
            equipped_sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("TOOL_Hoe");
            Debug.Log("Equipped: Hoe");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equipped_tool = "seed";
            equipped_sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Rose_Seed");
            Debug.Log("Equipped: Seed");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equipped_tool = "watering_can";
            equipped_sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("TOOL_Watering Can");
            Debug.Log("Equipped: Watering Can");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            equipped_tool = "hand";
            equipped_sprite.GetComponent<SpriteRenderer>().sprite = null;
            Debug.Log("Equipped: Hand / Pick Up");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            equipped_tool = "gun";
            equipped_sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("WEAPON_Pistol_Day");
            Debug.Log("Equipped: Shooty Shooty Bang Bang");
        }


        if (player.isRight)
        {
            rightHand.GetComponent<SpriteRenderer>().sprite = equipped_sprite.GetComponent<SpriteRenderer>().sprite;
            rightHand.GetComponent<SpriteRenderer>().flipX = false;
            rightHand.SetActive(true);
            leftHand.SetActive(false);
        }
        else if (!player.isRight)
        {
            leftHand.GetComponent<SpriteRenderer>().sprite = equipped_sprite.GetComponent<SpriteRenderer>().sprite;
            leftHand.GetComponent<SpriteRenderer>().flipX = true;
            leftHand.SetActive(true);
            rightHand.SetActive(false);
        }
        if (player.isUpwards)
        {
            
        }
        else if (!player.isUpwards)
        {
            
        }
    }

    void UseHoe()
    {
        if (inContact)
        {
            if (!soil.GetComponent<Soil>().occupied)
            {
                soil.GetComponent<Renderer>().material.color = tilled_soil_color;
                soil.GetComponent<Soil>().state = "Tilled";
            }
        }             
    }

    void UseSeed()
    {
        if (inContact)
        {
            if (soil.GetComponent<Soil>().state == "Tilled" && !soil.GetComponent<Soil>().occupied)
            {
                soil.GetComponent<Soil>().occupied = true;
                soil.GetComponent<Soil>().planted_crop = PlantTypes.Rose();
                soil.GetComponent<Renderer>().material.color = planted_soil_color;
                Debug.Log(soil.GetComponent<Soil>().planted_crop.name);
            }
        }       
    }

    void UseWateringCan()
    {
        if (inContact)
        {
            if (soil.GetComponent<Soil>().state == "Tilled")
            {
                soil.GetComponent<Renderer>().material.color = watered_soil_color;
                soil.GetComponent<Soil>().state = "Watered";

            }
        }     
    }

    void UseHand()
    {
        if (inContact)
        {
            if (soil.GetComponent<Soil>().planted_crop.state == "Third")
            {
                soil.transform.GetChild(0).gameObject.SetActive(false);
                soil.GetComponent<Renderer>().material.color = empty_soil_color;
                soil.GetComponent<Soil>().occupied = false;

            }
        }     
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision == null)
        {
            soil = null;
            inContact = false;
            Debug.Log("No Contact");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Soil")
        {
            soil = collision.gameObject;
            inContact = true;
            Debug.Log(soil.name);
        }

        if (collision.gameObject.tag == "Ground")
        {
            soil = null;
            inContact = false;
        }
    }
}
