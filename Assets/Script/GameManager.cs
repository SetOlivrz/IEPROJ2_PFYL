using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Status
{
    NEUTRAL,
    LOSE,
    WIN,
    HOLD
};
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField]PlayerData playerData;
    [SerializeField] ButtonManager uiManager;
    public Status gameState = Status.NEUTRAL;

    // Start is called before the first frame update
    void Start()
    {
        gameState = Status.NEUTRAL;
        if (player == null)
        {
            Debug.Log("Game manager cannot find player game object");
        }

        if (playerData == null)
        {
            Debug.Log("Game manager cannot find player data");
        }

        if (uiManager == null)
        {
            Debug.Log("Game manager cannot find ui manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 1 - Test")
        {
            //Assign
            player = GameObject.FindGameObjectWithTag("Player");
            playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
            uiManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
        }
        UpdateGameStatus();
        CheckGameStatus();
    }
    void UpdateGameStatus()
    {
        if (playerData.currHP <= 0 && gameState == Status.NEUTRAL)
        {
            gameState = Status.LOSE;
        }
    }

    void CheckGameStatus()
    {
        if (gameState == Status.LOSE)
        {
            //uiManager.GameOverPopup();
            //gameState = Status.HOLD;
            Debug.Log("Game paused manager");
        }
        else if (gameState == Status.WIN)
        {
            Debug.Log("Trigger next day");
        }
        else if (gameState == Status.NEUTRAL)
        {

        }
    }
}
