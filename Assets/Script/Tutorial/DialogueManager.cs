using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    [SerializeField] public GameObject panel;
    [SerializeField] public GameObject popUp;

    [SerializeField] public Button nextButton;

    [SerializeField] public Text dialogueText;
    [SerializeField] public Text nameText;

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

        nextButton.interactable = false;

        //StartCoroutine(IntroTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        if (startDelay == true)
        {
            ticks += Time.unscaledDeltaTime;

            if (ticks >= maxTicks) // enables the button after the specified sec delay
            {
                EnableButton();
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

        // display the first popup
        panel.SetActive(true);
        popUp.SetActive(true);

        PauseGame();
        DisableButton();

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
        DisableButton();

        if (nTutorialIndex == 1) // instruction for movemnet
        {
            dialogueText.text = "Press WASD to move around";
            startDelay = true;
            maxTicks = 5;

        }
        else if (nTutorialIndex ==2) // execution for player movement
        {
            popUp.SetActive(false);
            panel.SetActive(false);
            // resume
            ResumeGame();

            // set the item to pick up to active (from TAmanager script)
            TAmanager.parentContainer.SetActive(true);

            // step 2, has to be checked in da TA manager
            
        }
        else if (nTutorialIndex == 3)// intro to planting after movement tutorial
        {

            popUp.SetActive(true);
            panel.SetActive(true);
            PauseGame();
            DisableButton();
            dialogueText.text = " Now, let’s try planting ";

            startDelay = true;
            maxTicks = 5;
        }
        else if (nTutorialIndex ==4)// instruction on how to use a hoe
        {
            dialogueText.text = " Pressing num keys 1-6 to allows you to change the equipped tool or use item from you inventory.";
            startDelay = true;
            maxTicks = 3;

        }
        else if (nTutorialIndex == 5)
        {
            dialogueText.text = " For easier access, you may also perform this using the scroll wheel.";
            startDelay = true;
            maxTicks = 5;
        }
        // disables button when next if pressed

    }

    public void  EnableButton()
    {
        nextButton.interactable = true;
    }

    public void DisableButton()
    {
        nextButton.interactable = false;
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
        EnableButton();
    }
}
    
   