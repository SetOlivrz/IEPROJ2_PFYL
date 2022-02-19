using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    [SerializeField] public GameObject panel;
    [SerializeField] public GameObject popUp;

    [SerializeField] public GameObject videoPopup;


    [SerializeField] public Button nextButtonDial;
    [SerializeField] public Button nextButtonVid;


    [SerializeField] public Text dialogueText;
    [SerializeField] public Text videoText;


    [SerializeField] private GameObject image;
    [SerializeField] private List<Sprite> TutorialSpriteList;

    public int nTutorialIndex = 0;

    public float ticks = 0;
    public float maxTicks = 0;

    public bool startDelay = false;


    [SerializeField] TutorialActionManager TAmanager;

    // Start is called before the first frame update
    void Start()
    {
        //TutorialSpriteList = new List<Sprite>();
        nTutorialIndex = 0;
        panel.SetActive(false);
        popUp.SetActive(false);

        DisableButton(1);

        StartCoroutine(IntroTutorial());

    }

    // Update is called once per frame
    void Update()
    {
        if (startDelay == true)
        {
            ticks += Time.unscaledDeltaTime;

            if (ticks >= maxTicks) // enables the button after the specified sec delay
            {
                //if (nTutorialIndex == 3 || nTutorialIndex >=8 && nTutorialIndex <13)
               // {
                    EnableButton(2);
               // }
               /// else
               // {
                    EnableButton(1);
               // }

                startDelay = false;
                ticks = 0;
            }
        }
        else if (ticks != 0 && startDelay ==false)
        {
            ticks = 0;
        }
    }

    IEnumerator IntroTutorial()
    {
        // wait for 3 seconds
        yield return new WaitForSeconds(3);

        dialogueText.text = "Hello there nephew!";
        // display the first popup
        panel.SetActive(true);
        popUp.SetActive(true);

        PauseGame();
        DisableButton(1);

        maxTicks = 1.0f;
        startDelay = true;
        // wait for 5 seconds

        // make next button interactable
    }

    // call this function to proceed to the tutorial
    // case 1: when the player selects the next button
    // case 2: when the player satisfies the condition to trigger the next step in the tutorial
    public void ProceedTutorial()
    {
        nTutorialIndex++;
        // pause
        PauseGame();
        DisableButton(1);

        if (nTutorialIndex == 1)// dial 2
        {
            /* change dialogue 
             * add delay
             * enable next button
             */

            dialogueText.text = "During day time, you can grow plants in preparation for the skirmishes of monsters during night time";
            startDelay = true;
            maxTicks = 1;

        }
        else if (nTutorialIndex == 2)// dial 3
        { 
     
            PauseGame();
            DisableButton(1);
            dialogueText.text = "but before anything else, why don't you help me gather some seeds ";

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 3)// show video
        {

            popUp.SetActive(false);

            // show video popup
            videoText.text = "There are four red thorny seeds scattered around the farm. Press WASD to walk around and collect the seeds.  ";
            videoPopup.SetActive(true);
            ChangeImage("01_WASD");
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;


        }
        else if (nTutorialIndex ==4) // execution for player movement
        {
            videoPopup.SetActive(false);
            panel.SetActive(false);
            ResumeGame();

            // set the item to pick up to active (from TAmanager script)
            TAmanager.parentContainer.SetActive(true);
            // step 2, has to be checked in da TA manager
            
        }
        else if (nTutorialIndex == 5)// intro to planting after movement tutorial
        {

            popUp.SetActive(true);
            panel.SetActive(true);
            PauseGame();
            DisableButton(1);
            dialogueText.text = "Great! ";

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 6)// instruction on how to use a hoe
        {

            dialogueText.text = "Now that we have the seeds, let’s start planting";
            startDelay = true;
            maxTicks = 1;

        }
        else if (nTutorialIndex == 7)
        {
 
            dialogueText.text = "Planting can be intimidating at first but let me teach you the basics";
            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 8)
        {

            popUp.SetActive(false);
            videoText.text = "Displayed on the lower left of the screen is your hotbar";
            ChangeImage("02_Hotbar");
            videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex ==9)
        {

            popUp.SetActive(false);
            videoText.text = "Found inside are your equipments and items";
            ChangeImage("03_Equipments");
            videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 10)
        {
 
           // video: show hwo to access the hotbar
           videoText.text = "You can also access the inventory by pressing TAB or simply clicking the bag button found beside the hotbar. You may also drag and drop items from you inventory to you hotbar";
            ChangeImage("04_Inventory");
            videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 11)
        {

            // change video
            videoText.text = "You can equip item by using num keys and the scroll wheel.";
            ChangeImage("05_Equiping item");
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 12)
        {


            // change video
            videoText.text = "Press the left mouse button to use the currently equipped item. ";
            //videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 13)
        {


            // off videoPop up
            videoPopup.SetActive(false);

            // on popUp
            dialogueText.text = "In planting, tilling the soil comes first before anything else.";
            popUp.SetActive(true);

            PauseGame();
            DisableButton(1);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 14)
        {

            dialogueText.text = "Now why don't you try equipping the hoe tool and start tilling some soil";
          
            PauseGame();
            DisableButton(1);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 15)
        {
            popUp.SetActive(false);
            panel.SetActive(false);

            TAmanager.hasUsedHoe = false;
            ResumeGame();

            // check if the player has tilled the soil in TA manager update func (index 15)

        }
        else if (nTutorialIndex == 16)
        {
            popUp.SetActive(true);
            panel.SetActive(true);
            PauseGame();
            DisableButton(1);
            dialogueText.text = "Well done";

            startDelay = true;
            maxTicks = 3;
        }
        else if (nTutorialIndex == 17)
        {


            popUp.SetActive(true);
            panel.SetActive(true);
            PauseGame();
            DisableButton(1);
            dialogueText.text = "Now that you've tilled the soil, you can now plant the seed";

            startDelay = true;
            maxTicks = 3;
        }
        else if (nTutorialIndex == 18)
        {
            /* hide dial pop
             * show vid pop
             * update vid text
             * disable button
             * add delay
             * enable button
             */

            popUp.SetActive(false);
            videoPopup.SetActive(true);
            ChangeImage("06_Equip seed");

            videoText.text = "Equip the rose dagger seed from your inventory by pressing num key 3 or using the scroll wheel ";

            DisableButton(2);

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 19)
        {
            videoText.text = "After doing so, plant the seed in the tilled soil by pressing the left mouse button. ";
            ChangeImage("07_Plant seed");

            DisableButton(2);

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 20)
        {
            videoPopup.SetActive(false);
            popUp.SetActive(true);

            dialogueText.text = "Now, go ahead and try it. ";

            DisableButton(1);

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 21)
        {
            popUp.SetActive(false);
            panel.SetActive(false);

            ResumeGame();

            TAmanager.hasPlantedSeed = false;
            // check if the player has planted a seed in TA manager
        }
        else if (nTutorialIndex ==22)
        {
            popUp.SetActive(true);
            panel.SetActive(true);
            PauseGame();
            DisableButton(1);
            dialogueText.text = "Easy right?";

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 23)
        {
            DisableButton(1);
            dialogueText.text = "Now, what we need to do is water the plant. Equip the watering can from your inventory and start watering planted seed.";

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 24)
        {
            popUp.SetActive(false);
            panel.SetActive(false);

            ResumeGame();

            // check if the player has watered as plant
        }
        else if (nTutorialIndex == 25)
        {
            popUp.SetActive(true);
            panel.SetActive(true);
            PauseGame();
            DisableButton(1);
            dialogueText.text = "It will take time for the plant to fully grow. ";

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 26)
        {
            DisableButton(1);
            dialogueText.text = "Premature plants will display a timer that indicates its growth time. Meanwhile, plants that are ready for harvest has visible pollen particles floating around them  ";

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 27)
        {
            popUp.SetActive(false);
            panel.SetActive(false);
            ResumeGame();

            // check
          
        }
        else if (nTutorialIndex == 28)
        {
            popUp.SetActive(true);
            panel.SetActive(true);
            PauseGame();
            DisableButton(1);
            dialogueText.text = "Your crop has fully grown! Harvest them by pressing the left mouse button while standing close to them.";

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 29)
        {
       
            DisableButton(1);
            dialogueText.text = "Stock up your weapons and resources before night time begins so don't forget to pick up the drops as well";

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 30)
        {
            popUp.SetActive(false);
            panel.SetActive(false);

            ResumeGame();

            // check if the has picked up the plant
        }
        else if (nTutorialIndex == 31)
        {
            popUp.SetActive(true);
            panel.SetActive(true);
            PauseGame();
            DisableButton(1);
            dialogueText.text = "Oh no! The monsters are coming!. ";

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 32)
        {
            popUp.SetActive(false);
            videoPopup.SetActive(true);

            DisableButton(1);
            ChangeImage("10_Night time");

            videoText.text = "During night time, monsters will come to your farm and attack you (insert night image).";

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 33)
        {
            DisableButton(1);
            ChangeImage("11_Enemy");

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 34)
        {
            DisableButton(1);
            videoText.text = "You can kill monsters using a gun and a knife";
            ChangeImage("12_Weapons");
            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex ==35)
        {
            DisableButton(1);
            videoText.text = "Guns are good for ranged but takes time to reload";
            ChangeImage("13_Shoot");

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 36)
        {
            DisableButton(1);
            videoText.text = "Knives however are good for close combat. It can deal massive damage but gets destroyed upon single use ";
            ChangeImage("14_Slash");

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 37)
        {
            DisableButton(1);
            videoText.text = " both equipments are used upon pressing the left mouse button";

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 38)
        {
            DisableButton(1);
            videoText.text = "Monster deal damage once they get close enough to you";
            ChangeImage("15_Hurt");


            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 39)
        {
            DisableButton(1);
            videoPopup.SetActive(false);
            popUp.SetActive(true);
            dialogueText.text = "Now, go equip yourself with weapons and get those monsters before they get you.";

            startDelay = true;
            maxTicks = 0;
        }
        else if (nTutorialIndex == 40)
        {
            panel.SetActive(false);
            popUp.SetActive(false);
            ResumeGame();

            // check if the player has finished the stage
        }

    }

    public void  EnableButton(int key)
    {
        switch(key)
        {
            case 1: nextButtonDial.interactable = true; break;
            case 2: nextButtonVid.interactable = true; break;
        };
    }

    public void DisableButton(int key)
    {
        switch (key)
        {
            case 1: nextButtonDial.interactable = false; break;
            case 2: nextButtonVid.interactable = false; break;
        };
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    IEnumerator SecDelayBeforeNext(float sec)
    {
        yield return new WaitForSeconds(sec);
        EnableButton(1);
    }

    public void ChangeImage(string element)
    {
        Image imageComp = image.GetComponent<Image>();

        if (imageComp != null)
        {
            for (int i = 0; i < TutorialSpriteList.Count; i++)
            {
                if(element == TutorialSpriteList[i].name)
                {
                    imageComp.sprite = TutorialSpriteList[i];
                    Debug.Log("HAS FOUND: " + element);
                    return;
                }
            }
            Debug.Log("Kennat find comp");

        }
        else
        {
            Debug.Log("cannot find comp");

        }
    }
}
    
   