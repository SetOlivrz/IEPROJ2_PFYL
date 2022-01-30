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
    Plant plant;
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
        for (int i = 0; i < 3; i++)
        {
            // sprite change goes here
            plantSprite.sprite = plantSprites[(int)plant.GetPlantType()].plantSprites[i];

            Debug.Log(i + "count");
            //Timer happens 3 times
            yield return new WaitForSeconds(plant.GetGrowth() / 3);
        }

        Debug.Log("Finished Growing");
    }

    //Function used to harvest crop that is fully grown
    //Not fully implemented yet do not use!!!!!
    public void Harvest()
    {
        //Set sprite to inactive
        plant = GetComponentInChildren<Plant>();
        plant.gameObject.SetActive(false);
        isGrown = false;

        //Check if inventory is full
            //add item into inventory if not full
            //Drop item into ground if full
    }

    public void Till()
    {
        //code here that changes color of soil

        isTilled = true;

        Debug.Log("You have tilled the soil");
    }

    public void Plant(Seed seed)
    {
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
