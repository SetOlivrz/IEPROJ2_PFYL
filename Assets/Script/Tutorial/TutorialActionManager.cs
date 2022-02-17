using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActionManager : MonoBehaviour
{
    // Start is called before the first frame update

    // references: player, interactable GO, events for accomplishing steps in tutorial

    [SerializeField] GameObject player;
    [SerializeField] DialogueManager dialManager;

    // variables for the player movement guide 
    [SerializeField] public GameObject parentContainer;
    [SerializeField] List<GameObject> itemsToPickup = new List<GameObject>();
    public int nPickedUpItems = 0;

    public bool inTutorial = false;
    // variables foe the equipment hoe tutorial
    public bool hasUsedHoe = false;
    public bool hasPlantedSeed = false;
    public bool hasWateredPlant = false;
    public bool hasFullyGrown = false;

    public bool hasHarvested = false;


    public int currentStep = 0;


    void Start()
    {
        currentStep = dialManager.nTutorialIndex;
        parentContainer.SetActive(false); // hides the interactable items needed for the tutoria;
    }

    // Update is called once per frame
    void Update()
    {
        currentStep = dialManager.nTutorialIndex;

        if (currentStep == 4) // player movement tutorial
        {
            // check of the player picks up the items

            for (int i = 0; i < itemsToPickup.Count; i++)
            {
                if (itemsToPickup[i] == null)
                {
                    itemsToPickup.RemoveAt(i);
                    nPickedUpItems++;
                    Debug.Log("Picked up");

                }
            }
            // remove from the array/list once the item is picked up by the player

            if(itemsToPickup.Count == 0)
            {
                Debug.Log("Picked up all items: " + itemsToPickup.Count);
                itemsToPickup.Clear();
                dialManager.ProceedTutorial();
                // add delay
                // call the Proceed tutorial func
            }
        }
        else if (currentStep == 15) // tutorial on how to use the hoe
        {
            if (hasUsedHoe == true)
            {
                Debug.Log(" the player has used the hoe tool");
                dialManager.ProceedTutorial();
            }
        }
        else if (currentStep == 21)
        {
            if (hasPlantedSeed == true)
            {
                Debug.Log(" planted seed");
                dialManager.ProceedTutorial();
            }
        }
        else if (currentStep == 24)
        {
            if (hasWateredPlant && hasFullyGrown)
            {
                dialManager.ProceedTutorial();
            }
        }
        else if ( currentStep == 29)
        {
            if(hasHarvested == true)
            {
                Debug.Log(" the player has harvested the plant :D");
                dialManager.ProceedTutorial();
            }
        }
    }
}
