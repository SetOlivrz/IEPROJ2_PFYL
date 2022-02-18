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

        dialogueText.text = "Hello there Nephew, intro ...";
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
            /* pause game 
             * disable button
             * change text
             * add delay
             * enable next button
             */

            PauseGame();
            DisableButton(1);
            dialogueText.text = "but before anything else, why don't you help me gather some seeds ";

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 3)// show video
        {
            /* hide main dialogue popup
             * update vid pop text
             * pause game
             * disable next button
             * add dekay
             * enable next button
             * */

            popUp.SetActive(false);

            // show video popup
            videoText.text = "There are 4 red thorny seeds scattered around your farm. Press WASD to walk around and pick up the seeds.  ";
            videoPopup.SetActive(true);
            ChangeImage("01_WASD");
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;


        }
        else if (nTutorialIndex ==4) // execution for player movement
        {
            /* hide video pop
             * hide black panel
             * resume game
             * display seeds to pickup for player movement exec.
             */

            videoPopup.SetActive(false);
            panel.SetActive(false);
            ResumeGame();

            // set the item to pick up to active (from TAmanager script)
            TAmanager.parentContainer.SetActive(true);
            // step 2, has to be checked in da TA manager
            
        }
        else if (nTutorialIndex == 5)// intro to planting after movement tutorial
        {
            /* display main dialogue pop
            * display black panel
            * pause game
            * disable button
            * update text
            * add delay
            * enable button
           */

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
            /*change dialogue
             * add delay
             * enable button
             */

            dialogueText.text = "Now that we have the seeds, let’s start planting";
            startDelay = true;
            maxTicks = 1;

        }
        else if (nTutorialIndex == 7)
        {
            /*change dialogue
             * add delay
             * enable button
             */

            dialogueText.text = "Planting can be intimidating at first but let me teach you the basics";
            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 8)
        {
            /* hide pop
             * update vid text
             * display vid pop
             * pause game
             * disable button
             * add delay
             */

            popUp.SetActive(false);
            videoText.text = "Displayed on the lower left of you screen is your hotbar";
            ChangeImage("02_Hotbar");
            videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex ==9)
        {
            /* hide main dialogue pop
             * update vid text
             * show vid pop
             * pause game
             * disable button
             * add delay
             * enable button
             */

            // change video

            popUp.SetActive(false);
            videoText.text = "Found inside are your equipment and items";
            ChangeImage("03_Equipments");
            videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 10)
        {
            /*
            * update vid text
            * show vid pop
            * pause game
            * disable button
            * add delay
            * enable button
            */

           // video: show hwo to access the hotbar
           videoText.text = "You can also access the inventory by pressid TAB or simply clicking the bag button found beside the hotbar. You may also drag and drop items from you inventory to you hotbar";
            ChangeImage("04_Inventory");
            videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 11)
        {
            /*update vid text
           *show vid pop
           *pause game
           * disable button
           * add delay
           * enable button
           */

            // change video
            videoText.text = "You can equip item by using num keys and scroll wheel.";
            ChangeImage("05_Equiping item");
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 12)
        {
           /*update vid text
           *show vid pop
           *pause game
           * disable button
           * add delay
           * enable button
           */

            // change video
            videoText.text = "Press the left mouse button to use the currently equiped item. ";
            //videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 13)
        {
            /* hide video pop
             * update dial text
             * display main dial pop
             * pause game
             * disable button
             * add delay
             * enable button
             */

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
            /*change dialogue
            * add delay
            * enable button
            */

            dialogueText.text = "Now why don't you try equipping a hoe and start tilling some soil";
          
            PauseGame();
            DisableButton(1);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 15)
        {
            /* hide main dial pop
             * hide panel pop
             * resume game
             * check if the player has tilled a soil
             */

            popUp.SetActive(false);
            panel.SetActive(false);

            TAmanager.hasUsedHoe = false;
            ResumeGame();

            // check if the player has tilled the soil in TA manager update func (index 15)

        }
        else if (nTutorialIndex == 16)
        {
            /* display main dialogue pop
             * display black panel
             * pause game
             * disable button
             * update text
             * add delay
             * enable button
            */

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
            /* disable button
             * update text
             * add delay
             * enable button
            */

            popUp.SetActive(true);
            panel.SetActive(true);
            PauseGame();
            DisableButton(1);
            dialogueText.text = "Now that you've tilled the soil, you can now plant your seed";

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
            maxTicks = 1;
        }
        else if (nTutorialIndex == 19)
        {
            videoText.text = "After doing so, move close to the tilled soil and plant it by pressing the left mouse button. ";
            ChangeImage("07_Plant seed");

            DisableButton(2);

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 20)
        {
            videoPopup.SetActive(false);
            popUp.SetActive(true);

            dialogueText.text = "Now, go ahead and try planting one. ";

            DisableButton(1);

            startDelay = true;
            maxTicks = 1;
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
            maxTicks = 3;
        }
        else if (nTutorialIndex == 23)
        {
            DisableButton(1);
            dialogueText.text = "Now, what we need to do is water the plant. Equip the watering can from your inventory and start watering the plant.";

            startDelay = true;
            maxTicks = 3;
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
            maxTicks = 1;
        }
        else if (nTutorialIndex == 26)
        {
            DisableButton(1);
            dialogueText.text = "Premature plants will display a timer that indicates its growth time. Meanwhile, plants that are ready for harvest has visible pollen particles floating around them  ";

            startDelay = true;
            maxTicks = 1;
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
            maxTicks = 1;
        }
        else if (nTutorialIndex == 29)
        {
       
            DisableButton(1);
            dialogueText.text = "Stock up your weapons and resources before night time begins so don't forget to pick up the drops as well";

            startDelay = true;
            maxTicks = 1;
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
            maxTicks = 1;
        }
        else if (nTutorialIndex == 32)
        {
            popUp.SetActive(false);
            videoPopup.SetActive(true);

            DisableButton(1);
            videoText.text = "During night time, monsters will come to your farm and attack you (insert night image).";

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 33)
        {
            DisableButton(1);
            videoText.text = "(insert night image with monster).";

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 34)
        {
            DisableButton(1);
            videoText.text = "You can kill monsters using a gun and a knife";

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex ==35)
        {
            DisableButton(1);
            videoText.text = "Guns are good for ranged but takes time to reload";

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 36)
        {
            DisableButton(1);
            videoText.text = "Knives however are good for close combat. It can one hit an enemy but gets destroyed upon single use ";

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 37)
        {
            DisableButton(1);
            videoText.text = " both equipments are used upon pressing the left mouse button";

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 38)
        {
            DisableButton(1);
            videoText.text = "Monster deal damage once they get close enough to you";

            startDelay = true;
            maxTicks = 1;
        }
        else if (nTutorialIndex == 39)
        {
            DisableButton(1);
            videoText.text = "So equip yourself with those weapons and get those monsters before they get you.";

            startDelay = true;
            maxTicks = 1;
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
    
   