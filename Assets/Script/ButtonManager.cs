using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject blackPanel;
    [SerializeField] GameObject newGamePopup;
    [SerializeField] GameObject exitPopup;

    [SerializeField] GameObject overwritePopup;
    [SerializeField] GameObject deletePopup;
    [SerializeField] GameObject TutorialPopup;

    [SerializeField] GameObject pausePopUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGameConfimation()
    {

        if (this.newGamePopup.activeInHierarchy == false)
        {
            this.blackPanel.SetActive(true);
            this.newGamePopup.SetActive(true);
        }
        else
        {
            this.blackPanel.SetActive(false);
            this.newGamePopup.SetActive(false);
        }
    }

    public void ExitGameConfirmation()
    {
        Debug.Log("Exit");

        if (this.exitPopup.activeInHierarchy == false)
        {
            this.blackPanel.SetActive(true);
            this.exitPopup.SetActive(true);
        }
        else
        {
            this.blackPanel.SetActive(false);
            this.exitPopup.SetActive(false);
        }
    }

    public void ExitApp()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void CreateAccount()
    {
        Debug.Log("Create Account");
        SceneManager.LoadScene("NewAccount");
    }

    public void ReturnToMainmenu()
    {
        Debug.Log("Return to Mainmenu");
        SceneManager.LoadScene("Mainmenu");
    }

    public void StartNewGame()
    {
        Debug.Log("Load Selected Game File");
        SceneManager.LoadScene("Level 1");
    }

    public void OverwriteConfimation()
    {

        if (this.overwritePopup.activeInHierarchy == false)
        {
            this.blackPanel.SetActive(true);
            this.overwritePopup.SetActive(true);
        }
        else
        {
            this.blackPanel.SetActive(false);
            this.overwritePopup.SetActive(false);
        }
    }

    public void DeleteConfirmation()
    {
        if (this.deletePopup.activeInHierarchy == false)
        {
            this.blackPanel.SetActive(true);
            this.deletePopup.SetActive(true);
        }
        else
        {
            this.blackPanel.SetActive(false);
            this.deletePopup.SetActive(false);
        }
    }

    public void TutorialConfirmation()
    {
        if (this.TutorialPopup.activeInHierarchy == false)
        {
            this.TutorialPopup.SetActive(true);
        }
        else
        {
            OverwriteConfimation();
            this.TutorialPopup.SetActive(false);
        }
    }

    public void DeleteFile()
    {
       //[DLELETE FILE ALGO]
       //1. delete file selected
       //2. trigget a popup that says the file is already deleted
       //3. minimize the pop up
        Debug.Log("File Deleted");
        this.blackPanel.SetActive(false);
        this.deletePopup.SetActive(false);
    }

    public void OverwriteNStartGame()
    {

        // [SAVE NEW GAME AND LOAD NEW GAME ALGO]
        // 1. trigger pop us that says that the game is already loaded
        // 2. load a new game
        Debug.Log("Overwrite Data");
    }

    public void StartTutorialLevel()
    {
        Debug.Log("Tutorial Level");
        SceneManager.LoadScene("Level 1");
    }

    public void StartNormalLevel()
    {
        Debug.Log("Normal Level");
        SceneManager.LoadScene("Level 1");
    }


    public void PauseGame()
    {
        if (pausePopUp.activeInHierarchy == false)
        {
            Debug.Log("Game Paused");

            Time.timeScale = 0;
            this.blackPanel.SetActive(true);
            this.pausePopUp.SetActive(true);
        }
        else
        {
            this.blackPanel.SetActive(false);
            this.pausePopUp.SetActive(false);
        }
    }
    public void ResumeGame()
    {
        PauseGame();
        Debug.Log("Game Resumed");
        Time.timeScale = 1;
    }

    public void AccessInventory()
    {
        Debug.Log("Open Inventory");
    }
}
