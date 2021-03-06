using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject blackPanel;
    [SerializeField] GameObject blackPanel2;
    [SerializeField] GameObject blackPanel3;



    [SerializeField] GameObject newGamePopup;
    [SerializeField] GameObject exitPopup;

    [SerializeField] GameObject createPopup;

    [SerializeField] GameObject overwritePopup;
    [SerializeField] GameObject loadPopup;

    [SerializeField] GameObject deletePopup;
    [SerializeField] GameObject TutorialPopup;
    [SerializeField] GameObject loadConfirmationPopup;

    [SerializeField] GameObject pausePopUp;
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] GameObject stageClearPopup;

    [SerializeField] PlayerData playerData;
    [SerializeField] GameManager manager;


    [SerializeField] Player player;

    [Header("Audio")]
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject mute;
    [SerializeField] GameObject unmute;

    //Enemy
    [SerializeField] GameObject enemySpawner;

    void Update()
    {
        //Assign
        if (SceneManager.GetActiveScene().name == "Level 1 - Test")
            manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        else manager = null;
    }
    public void NewGameConfimation()
    {
        if (this.newGamePopup.activeInHierarchy == false) // main panel disabled
        {
           
            this.newGamePopup.SetActive(true); // newgame pp
            this.newGamePopup.GetComponent<PanelOpener>().OpenPanel();
        }
        else
        {
            this.newGamePopup.GetComponent<PanelOpener>().OpenPanel();
        }

        if (blackPanel.activeInHierarchy == false)
        {
            BPfadeIn(blackPanel);
        }
        else
        {
            BPfadeOut(blackPanel);
        }
    }

    public void ExitGameConfirmation()
    {
        if (this.exitPopup.activeInHierarchy == false) // main panel disabled
        {

            this.exitPopup.SetActive(true); // newgame pp
            this.exitPopup.GetComponent<PanelOpener>().OpenPanel();
        }
        else
        {
            this.exitPopup.GetComponent<PanelOpener>().OpenPanel();
        }

        if (blackPanel.activeInHierarchy == false)
        {
            BPfadeIn(blackPanel);
        }
        else
        {
            BPfadeOut(blackPanel);
        }
    }

    public void ExitApp()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void CreateAccount()
    {

        this.newGamePopup.GetComponent<PanelOpener>().ClosePopup();
   

        if (this.createPopup.activeInHierarchy == false) // main panel disabled
        {
         
            this.createPopup.SetActive(true); // newgame pp
            this.createPopup.GetComponent<PanelOpener>().OpenPanel();
        }
        else
        {
            this.createPopup.GetComponent<PanelOpener>().OpenPanel();
            BPfadeOut(blackPanel);
        }

        if (blackPanel2.activeInHierarchy == false)
        {
            BPfadeIn(blackPanel2);
        }
        else
        {
            BPfadeOut(blackPanel2);
        }
    }

    public void LoadGameMenu()
    {

        if (this.loadPopup.activeInHierarchy == false) // main panel disabled
        {

            this.loadPopup.SetActive(true); // newgame pp
            this.loadPopup.GetComponent<PanelOpener>().OpenPanel();
        }
        else
        {
            this.loadPopup.GetComponent<PanelOpener>().OpenPanel();
        }

        if (blackPanel.activeInHierarchy == false)
        {
            BPfadeIn(blackPanel);
        }
        else
        {
            BPfadeOut(blackPanel);
        }
       // Debug.Log("Open load game menu");
        //SceneManager.LoadScene("LoadMenu");
    }

    public void ReturnToMainmenu()
    {
        //manager.gameState = Status.NEUTRAL;
        Time.timeScale = 1;
        TimeBehavior.day = 1;
        TimeBehavior.hour = 0;
        Debug.Log("Return to Mainmenu");
        AudioManager.instance.OnMain();
        SceneManager.LoadScene("Mainmenu 1");
    }

    public void StartNewGame()
    {
        Debug.Log("Load Created Game File");
        SceneManager.LoadScene("Level 1");
    }

    public void StartLoadGame()
    {
        Debug.Log("Load Selected Game File");
        manager.gameState = Status.NEUTRAL;
        SceneManager.LoadScene("Level 1 - Test");
    }

    public void LoadConfimation()
    {

        //if (this.loadPopup.activeInHierarchy == false)
        //{
        //    this.blackPanel.SetActive(true);
        //    this.loadPopup.SetActive(true);
        //}
        //else
        //{
        //    this.blackPanel.SetActive(false);
        //    this.loadPopup.SetActive(false);
        //}

        if (this.loadConfirmationPopup.activeInHierarchy == false)
        {
            BPfadeIn(blackPanel3);
            this.loadConfirmationPopup.SetActive(true);
            this.loadConfirmationPopup.GetComponent<Popup>().OpenPopup(); // animation
        }
        else
        {
            //this.blackPanel3.SetActive(false);
            this.loadConfirmationPopup.SetActive(false);
            //this.overwritePopup.GetComponent<Popup>().OpenPopup(); // animation
            BPfadeOut(blackPanel3);
        }

        if (blackPanel3.activeInHierarchy == false)
        {
            BPfadeIn(blackPanel3);
        }
        else
        {
            BPfadeOut(blackPanel3);
        }
    }

    public void OverwriteConfimation()
    {

        if (this.overwritePopup.activeInHierarchy == false)
        {
            //this.blackPanel3.SetActive(true);
            this.overwritePopup.SetActive(true);
            this.overwritePopup.GetComponent<Popup>().OpenPopup(); // animation
        }
        else
        {
            //this.blackPanel3.SetActive(false);
            this.overwritePopup.SetActive(false);

            //this.overwritePopup.GetComponent<Popup>().OpenPopup(); // animation

            //this.overwritePopup.SetActive(false);
        }

        if (blackPanel3.activeInHierarchy == false)
        {
            BPfadeIn(blackPanel3);
        }
        else
        {
            BPfadeOut(blackPanel3);
        }
    }

    public void DeleteConfirmation()
    {
        if (this.deletePopup.activeInHierarchy == false)
        {
            //this.blackPanel3.SetActive(true);
            this.deletePopup.SetActive(true);
            this.deletePopup.GetComponent<Popup>().OpenPopup(); // animation
        }
        else
        {
            //this.blackPanel3.SetActive(false);
            this.deletePopup.SetActive(false);

            //this.overwritePopup.GetComponent<Popup>().OpenPopup(); // animation

            BPfadeOut(blackPanel3);
        }

        if (blackPanel3.activeInHierarchy == false)
        {
            BPfadeIn(blackPanel3);
        }
        else
        {
            BPfadeOut(blackPanel3);
        }
    }

    public void TutorialConfirmation()
    {
        this.overwritePopup.SetActive(false);
        if (this.TutorialPopup.activeInHierarchy == false)
        {
            this.TutorialPopup.SetActive(true);
        }
        else
        {
            
            this.TutorialPopup.SetActive(false);
            blackPanel3.SetActive(false);
        }
    }

    public void DeleteFile()
    {
       //[DLELETE FILE ALGO]
       //1. delete file selected
       //2. trigget a popup that says the file is already deleted
       //3. minimize the pop up
        Debug.Log("File Deleted");
        this.blackPanel3.SetActive(false);
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
        SceneManager.LoadScene("Tutorial Scene");
        Time.timeScale = 1;

    }

    public void StartNormalLevel()
    {
        Debug.Log("Normal Level");
        SceneManager.LoadScene("Level 1 - Test");
        Time.timeScale = 1;

    }

    // game proper ui functions
    public void PauseGame()
    {

        if (this.pausePopUp.activeInHierarchy == false) // main panel disabled
        {
            
            Debug.Log("Game Paused");
            //Time.timeScale = 0;
            this.pausePopUp.SetActive(true); // newgame pp
            this.pausePopUp.GetComponent<PanelOpener>().OpenPanel();
        }
        else
        {
            this.pausePopUp.GetComponent<PanelOpener>().OpenPanel();
        }

        if (blackPanel.activeInHierarchy == false)
        {
            BPfadeIn(blackPanel);
        }
        else
        {
            BPfadeOut(blackPanel);
        }
        //AudioManager.instance.OnGamePause();
    }

    public void ResumeGame()
    {
        PauseGame();
        Debug.Log("Game Resumed");
        //AudioManager.instance.OnGameResume();
        Time.timeScale = 1;
        
    }
    //To fix UI update of buttons && slider
    public void Options()
    {
        if (this.optionsPanel.activeInHierarchy == false) // main panel disabled
        {

            this.optionsPanel.SetActive(true); // newgame pp
            this.optionsPanel.GetComponent<PanelOpener>().OpenPanel();
        }
        else
        {
            this.optionsPanel.GetComponent<PanelOpener>().OpenPanel();
        }

        if (blackPanel.activeInHierarchy == false)
        {
            BPfadeIn(blackPanel);
        }
        else
        {
            BPfadeOut(blackPanel);
        }
        // checker for if and when the options menu is accessed in actual game
        if (AudioManager.instance.isMuted)
        {
            Mute();
        }
        else
        {
            Unmute();
        }
    }


    public void OptionsPopup()
    {
        if (this.optionsPanel.activeInHierarchy == false) // main panel disabled
        {

            this.optionsPanel.SetActive(true); // newgame pp
           // this.optionsPanel.GetComponent<PanelOpener>().OpenPanel();
        }
        else
        {
            // this.optionsPanel.GetComponent<PanelOpener>().OpenPanel();
            this.optionsPanel.SetActive(false); // newgame pp

        }


        if (blackPanel2.activeInHierarchy == false)
        {
            blackPanel2.SetActive(true);
        }
        else
        {
            blackPanel2.SetActive(false);
        }
        // checker for if and when the options menu is accessed in actual game
        if (AudioManager.instance.isMuted)
        {
            Mute();
        }
        else
        {
            Unmute();
        }
    }

    public void Mute()
    {
        mute.gameObject.GetComponent<Button>().interactable = false;
        unmute.gameObject.GetComponent<Button>().interactable = true;
    }

    public void Unmute()
    {
        mute.gameObject.GetComponent<Button>().interactable = true;
        unmute.gameObject.GetComponent<Button>().interactable = false;
    }

    public void MuteButton()
    {
        if (AudioManager.instance.isMuted)
        {
            AudioManager.instance.OnMute(false);
        }
        else
        {
            AudioManager.instance.OnMute(true);
        }
    }
    public void AccessInventory()
    {
        Debug.Log("Open Inventory");

        player.OpenInventory();
     
    }

    public void RestartDay()
    {
        playerData.currHP = playerData.maxHP;
        manager.gameState = Status.NEUTRAL;
        
        Debug.Log("Restart Day");
        SceneManager.LoadScene("Level 1 - Test");
        Time.timeScale = 1;
        gameOverPopup.SetActive(false);
        stageClearPopup.SetActive(false);
        enemySpawner.GetComponent<EnemySpawning>().ResetAll();
        TimeBehavior.day = 1;
        TimeBehavior.hour = 0;
    }

    public void GameOverPopup()
    {
        if (gameOverPopup.activeInHierarchy == false)
        {
            Debug.Log("Game Paused");

            Time.timeScale = 0;
            this.blackPanel.SetActive(true);
            this.gameOverPopup.SetActive(true);
        }
        else
        {
            this.blackPanel.SetActive(false);
            this.gameOverPopup.SetActive(false);
        }
    }

    public void BPfadeIn(GameObject obj)
    {
        
        obj.SetActive(true);
        obj.GetComponent<FadeVFX>().panelState = FadeVFX.PanelState.FadeIn;
        Debug.Log("CHANGE STATE");
    }

    public void BPfadeOut(GameObject obj)
    {
        obj.GetComponent<FadeVFX>().panelState = FadeVFX.PanelState.FadeOut;
    }
}
