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

    bool locked = false;


    void Start()
    {
        currentStep = dialManager.nTutorialIndex;
        parentContainer.SetActive(false); // hides the interactable items needed for the tutoria;
    }

    // Update is called once per frame
    void Update()
    {
        currentStep = dialManager.nTutorialIndex;

        if (locked == false)
        {
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

                if (itemsToPickup.Count == 0 && locked == false)
                {
                    Debug.Log("Picked up all items: " + itemsToPickup.Count);
                    itemsToPickup.Clear();
                    // dialManager.ProceedTutorial();
                    locked = true;
                    StartCoroutine(PopUpDelay());
                }
            }
            else if (currentStep == 15) // tutorial on how to use the hoe
            {
                if (hasUsedHoe == true)
                {
                    Debug.Log(" the player has used the hoe tool");
                    locked = true;
                    StartCoroutine(PopUpDelay());
                }
            }
            else if (currentStep == 21)
            {
                if (hasPlantedSeed == true)
                {
                    Debug.Log(" planted seed");
                    locked = true;
                    StartCoroutine(PopUpDelay());
                }
            }
            else if (currentStep == 24)
            {
                if (hasWateredPlant)
                {
                    locked = true;
                    StartCoroutine(PopUpDelay());
                }

            }
            else if (currentStep == 27)
            {
                if (hasFullyGrown == true)
                {
                    locked = true;
                    StartCoroutine(PopUpDelay());
                }
            }
            else if (currentStep == 29)
            {
                if (hasHarvested == true)
                {
                    Debug.Log(" the player has harvested the plant :D");
                    locked = true;
                    StartCoroutine(PopUpDelay());
                }
            }
            else if (currentStep == 30)
            {
                if (TimeBehavior.isDaytime == false)
                {
                    Debug.Log(" Make it nightttttt");
                    locked = true;
                    StartCoroutine(PopUpDelay());
                }
            }
        }
       
    }
    IEnumerator PopUpDelay(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        dialManager.ProceedTutorial();
        Debug.Log("call next phase");
        locked = false;
    }

    IEnumerator PopUpDelay()
    {
        yield return new WaitForSecondsRealtime(2);
        dialManager.ProceedTutorial();
        Debug.Log("call next phase");
        locked = false;
    }
}