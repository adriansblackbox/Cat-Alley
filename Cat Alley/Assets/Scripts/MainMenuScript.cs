using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject GOMenu;
    public bool isPaused;
    public bool instructionsOpened;
    public bool optionsOpened;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        if (isPaused == false)
        {
            player.GetComponent<PlayerController>().enabled = false;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isPaused = true;
        } else
        {
            player.GetComponent<PlayerController>().enabled = true;
            resetPauseMenu();
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPaused = false;
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

    public void pauseInstructions()
    {
        if (!instructionsOpened)
        {
            pauseMenu.transform.GetChild(1).gameObject.SetActive(false);
            pauseMenu.transform.GetChild(2).gameObject.SetActive(true);
            instructionsOpened = true;
        } else
        {
            pauseMenu.transform.GetChild(1).gameObject.SetActive(true);
            pauseMenu.transform.GetChild(2).gameObject.SetActive(false);
            instructionsOpened = false;
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
        mainMenu.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }

    public void resetPauseMenu()
    {
        instructionsOpened = false;
        pauseMenu.transform.GetChild(1).gameObject.SetActive(true);
        pauseMenu.transform.GetChild(2).gameObject.SetActive(false);
    }
    
    public void CloseGame()
    {
        Application.Quit();
    }
}
