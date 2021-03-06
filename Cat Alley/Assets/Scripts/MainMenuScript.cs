using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject GOMenu;
    public GameObject Score;
     public GameObject CrossHair;
    public GameObject gStateManager;
    public bool isPaused;
    public bool instructionsOpened;
    public bool optionsOpened;
    private bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        Score.SetActive(false);
        CrossHair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted && !gStateManager.GetComponent<GameStateManager>().GameOver)
        {
            ToggleMenu();
        }else if(gStateManager.GetComponent<GameStateManager>().GameOver){
            CrossHair.SetActive(false);
            FindObjectOfType<GunMovement>().enabled = false;
        }
    }

    public void ToggleMenu()
    {
        if (isPaused == false)
        {
            player.GetComponent<PlayerController>().enabled = false;
            FindObjectOfType<GunMovement>().enabled = false;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isPaused = true;
            gStateManager.GetComponent<GameStateManager>().inGame = false;
        } else
        {
            EventSystem.current.SetSelectedGameObject(null);
            player.GetComponent<PlayerController>().enabled = true;
            FindObjectOfType<GunMovement>().enabled = true;
            resetPauseMenu();
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPaused = false;
            gStateManager.GetComponent<GameStateManager>().inGame = true;
        }
    }

    public void mainOptions()
    {
        if (!optionsOpened)
        {
            mainMenu.transform.GetChild(1).gameObject.SetActive(false);
            mainMenu.transform.GetChild(2).gameObject.SetActive(true);
            mainMenu.transform.GetChild(3).gameObject.SetActive(false);
            optionsOpened = true;
        } else
        {
            mainMenu.transform.GetChild(1).gameObject.SetActive(true);
            mainMenu.transform.GetChild(2).gameObject.SetActive(false);
            mainMenu.transform.GetChild(3).gameObject.SetActive(false);
            optionsOpened = false;
        }
    }

    public void mainInstructions()
    {
        if (!instructionsOpened)
        {
            mainMenu.transform.GetChild(1).gameObject.SetActive(false);
            mainMenu.transform.GetChild(2).gameObject.SetActive(false);
            mainMenu.transform.GetChild(3).gameObject.SetActive(true);
            instructionsOpened = true;
        }
        else
        {
            mainMenu.transform.GetChild(1).gameObject.SetActive(true);
            mainMenu.transform.GetChild(2).gameObject.SetActive(false);
            mainMenu.transform.GetChild(3).gameObject.SetActive(false);
            instructionsOpened = false;
        }
    }

    public void pauseOptions()
    {
        if (!optionsOpened)
        {
            pauseMenu.transform.GetChild(1).gameObject.SetActive(false);
            pauseMenu.transform.GetChild(2).gameObject.SetActive(false);
            pauseMenu.transform.GetChild(3).gameObject.SetActive(true);
            optionsOpened = true;
        } else
        {
            pauseMenu.transform.GetChild(1).gameObject.SetActive(true);
            pauseMenu.transform.GetChild(2).gameObject.SetActive(false);
            pauseMenu.transform.GetChild(3).gameObject.SetActive(false);
            optionsOpened = false;
        }
    }

    public void GOInstructions()
    {
        if (!instructionsOpened)
        {
            GOMenu.transform.GetChild(0).gameObject.SetActive(false);
            GOMenu.transform.GetChild(1).gameObject.SetActive(true);
            instructionsOpened = true;
        } else
        {
            GOMenu.transform.GetChild(0).gameObject.SetActive(true);
            GOMenu.transform.GetChild(1).gameObject.SetActive(false);
            instructionsOpened = false;
        }
    }

    public void StartGame()
    {
        FindObjectOfType<GunMovement>().enabled = true;
        gameStarted = true;
        Score.SetActive(true);
        CrossHair.SetActive(true);
        mainMenu.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
        gStateManager.GetComponent<GameStateManager>().inGame = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }

    public void resetPauseMenu()
    {
        optionsOpened = false;
        pauseMenu.transform.GetChild(1).gameObject.SetActive(true);
        pauseMenu.transform.GetChild(2).gameObject.SetActive(false);
        pauseMenu.transform.GetChild(3).gameObject.SetActive(false);
    }
    
    public void CloseGame()
    {
        Application.Quit();
    }
}
