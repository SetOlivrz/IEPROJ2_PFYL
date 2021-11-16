using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject blackPanel;
    [SerializeField] GameObject newGamePopup;
    [SerializeField] GameObject exitPopup;
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

    public void StartGame()
    {
        Debug.Log("Exit game");
        SceneManager.LoadScene("Level 1");
    }
}
