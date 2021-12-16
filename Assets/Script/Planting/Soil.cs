using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    bool isInRange = false;
    bool isGrown = false;

    //seems redundant, will replace later when inventory system has been implemented
    Plant plant;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Trigger enter and exit are to check if player is near enough to plant on specific plot
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isInRange = true;
            Debug.Log("Player in range");
            //Get player's inventory
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isInRange = false;
            Debug.Log("Player no longer in range");
        }
    }

    private void OnMouseUp()
    {
        if (isInRange)
        {
            plant = GetComponentInChildren<Plant>();

            //Check to see if plot is empty or not
            if (!plant.gameObject.activeSelf)
            {
                //Check if using seed and is not occupied (need:item with tag "Seed")


                //another check to see what seed it is (need: "Seed" item has "SeedType" enum)

                //Default for now since there's no inventory system
                plant.gameObject.SetActive(true);

                plant.type = Plant.PlantType.Rose;



                Debug.Log("Planted");
                StartCoroutine(Grow());
            }
            //Check if plot is occupied and ready to harvest
            else if (isGrown)
            {
                Harvest();
            }
        }
    }

    //Grow coroutine for the plants
    IEnumerator Grow()
    {
        for (int i = 0; i < 3; i++)
        {
            // sprite changing goes here

            //Timer happens 3 times
            yield return new WaitForSeconds(plant.growthSpeed / 3);
        }

        Debug.Log("Finished Growing");
    }

    //Function used to harvest crop that is fully grown
    void Harvest()
    {
        //Set sprite to inactive
        plant = GetComponentInChildren<Plant>();
        plant.gameObject.SetActive(false);
        isGrown = false;

        //Check if inventory is full
            //add item into inventory if not full
            //Drop item into ground if full
    }
}
