using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActionManager : MonoBehaviour
{
    // Start is called before the first frame update

    // references: player, interactable GO, events for accomplishing steps in tutorial

    [SerializeField] GameObject player;

    [SerializeField] public GameObject parentContainer;
    [SerializeField] List<GameObject>itemsToPickup =  new List<GameObject>();

    [SerializeField] DialogueManager dialManager;

    public int nPickedUpItems = 0;

    int currentStep = 0;


    void Start()
    {
        currentStep = dialManager.nTutorialIndex;
        parentContainer.SetActive(false); // hides the interactable items needed for the tutoria;
    }

    // Update is called once per frame
    void Update()
    {
        currentStep = dialManager.nTutorialIndex;

        if (currentStep == 2)
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
    }
}
