using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{   
    bool isGrown = false;
    bool hasSeed = false;
    bool isTilled = false;

    //seems redundant, will replace later when inventory system has been implemented
    public GameObject plantObject;
    public Plant plant;
    public List<ItemDrops> seedDrops;
    SpriteRenderer plantSprite;


    public List<PlantSprites> plantSprites = new List<PlantSprites>();

    // Start is called before the first frame update
    void Start()
    {
        plant = plantObject.GetComponent<Plant>();
        plantSprite = plantObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Grow coroutine for the plants
    IEnumerator Grow()
    {
        for (int i = 1; i < 3; i++)
        {
            yield return new WaitForSeconds(plant.GetGrowth() / 2);
            // sprite change goes here
            plantSprite.sprite = plantSprites[(int)plant.GetPlantType()].plantSprites[i];

            Debug.Log(i + "count");
            //Timer happens 3 times
        }

        gameObject.GetComponent<MeshRenderer>().material.color = new Color32(212, 212, 212, 255);
        isGrown = true;
        Debug.Log("Finished Growing");
    }

    //Function used to harvest crop that is fully grown
    //Not fully implemented yet do not use!!!!!
    public void Harvest()
    {
        //Set sprite to inactive
        plantObject.SetActive(false);
        isGrown = false;
        hasSeed = false;
        isTilled = false;

        Debug.Log("Harvested");
    }

    public void Till()
    {
        //code here that changes color of soil
        gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;

        isTilled = true;

        Debug.Log("You have tilled the soil");
    }

    public void Plant(Seed seed)
    {
        hasSeed = true;
        //set the plant based on seed
        plant.SetPlant(seed);

        //stuff here to show the plant on the soil
        plantSprite.sprite = plantSprites[(int)plant.GetPlantType()].plantSprites[0];
        plantObject.SetActive(true);

        Debug.Log("You have planted something");
    }

    public void Water()
    {
        //code here that changes color of soil
        gameObject.GetComponent<MeshRenderer>().material.color = new Color32(55, 55, 55, 255);

        //not sure what to do here
        // Plant will now start to grow
        StartCoroutine(Grow());

        Debug.Log("Watered the soil");
    }

    public bool GetIsTilled()
    {
        return isTilled;
    }

    public bool GetHasSeed()
    {
        return hasSeed;
    }

    public void SetHasSeed(bool hasSeed)
    {
        this.hasSeed = hasSeed;
    }

    public bool GetIsGrown()
    {
        return isGrown;
    }

    public void SetIsGrown(bool isGrown)
    {
        this.isGrown = isGrown;
    }
}
