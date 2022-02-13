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

    public int nTutorialIndex = 0;

    public float ticks = 0;
    public float maxTicks = 0;

    public bool startDelay = false;


    [SerializeField] TutorialActionManager TAmanager;

    // Start is called before the first frame update
    void Start()
    {
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
                if (nTutorialIndex == 3 || nTutorialIndex >=8 && nTutorialIndex <13)
                {
                    EnableButton(2);
                }
                else
                {
                    EnableButton(1);
                }

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

        maxTicks = 5.0f;
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
            maxTicks = 2;

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
            dialogueText.text = " but before anything else, why don't you help me gather some seeds ";

            startDelay = true;
            maxTicks = 2;
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
            videoText.text = " You saw 4 red thorny seed scattered around your farm. Press WASD to walk around and pick up the seeds.  ";
            videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 2;


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
            dialogueText.text = " Great! ";

            startDelay = true;
            maxTicks = 3;
        }
        else if (nTutorialIndex == 6)// instruction on how to use a hoe
        {
            /*change dialogue
             * add delay
             * enable button
             */

            dialogueText.text = "Now that we have the seeds, let’s start planting";
            startDelay = true;
            maxTicks = 3;

        }
        else if (nTutorialIndex == 7)
        {
            /*change dialogue
             * add delay
             * enable button
             */

            dialogueText.text = "Planting can be intimidating at first but let me teach you the basics";
            startDelay = true;
            maxTicks = 3;
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
            videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 2;
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
            videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 2;
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
           videoText.text = " You can access these by pressing num 1-6 or simply using the mouse scroll wheel";
            videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 2;
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
            videoText.text = "You can equip the hoe by pressing ______";
            //videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 2;
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
            videoText.text = " While equipping the hoe, you can till a soil by right clicking on it. ";
            //videoPopup.SetActive(true);
            PauseGame();
            DisableButton(2);

            startDelay = true;
            maxTicks = 2;
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
            dialogueText.text = "Tilling the soil comes first before anything else when planting.";
            popUp.SetActive(true);

            PauseGame();
            DisableButton(1);

            startDelay = true;
            maxTicks = 2;
        }
        else if (nTutorialIndex == 14)
        {
            /*change dialogue
            * add delay
            * enable button
            */

            dialogueText.text = "Now go ahead and try it out.";
          
            PauseGame();
            DisableButton(1);

            startDelay = true;
            maxTicks = 2;
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
            ResumeGame();

            // check if the player has tilled the soil in TA manager update func (index 15)

        }
        //// disables button when next if pressed
        //else if(nTutorialIndex == 8) //player uses a hoe
        //{
        //    popUp.SetActive(false);
        //    panel.SetActive(false);
        //    // resume
        //    ResumeGame();d

        //    // player uses the hoe
        //    // set the item to pick up to active (from TAmanager script)
        //    TAmanager.parentContainer.SetActive(true);

        //    // step 2, has to be checked in da TA manager
        //}
        //else if (nTutorialIndex == 9) //instruction for planting seed
        //{
        //    popUp.SetActive(true);
        //    panel.SetActive(true);
        //    PauseGame();
        //    DisableButton();
        //    dialogueText.text = " Press _____ to open your inventory and drag the Rose Thorn Dagger seed to the hot bar ";

        //    startDelay = true;
        //    maxTicks = 5;
        //}
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
}
    
   