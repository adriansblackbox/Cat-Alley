using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    //list of buttons in the menu
    public GameObject menu;
    public GameObject controlButton;
    public GameObject quitButton;
    public GameObject instructionText;
    public GameObject returnButton;

    //player to disable aiming while in the menu
    public GameObject player;

    public bool isPaused = false;
    private bool instructionsOpened = false;
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
            Time.timeScale = 0;
            player.GetComponent<PlayerController>().enabled = false;
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
        } else
        {
            player.GetComponent<PlayerController>().enabled = true;
            Time.timeScale = 1;
            menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
        }
    }

    public void ControlsOnClick()
    {
        if (!instructionsOpened)
        {
            controlButton.SetActive(false);
            quitButton.SetActive(false);
            instructionText.SetActive(true);
            returnButton.SetActive(true);
            instructionsOpened = true;
        }
        else
        {
            controlButton.SetActive(true);
            quitButton.SetActive(true);
            instructionText.SetActive(false);
            returnButton.SetActive(false);
            instructionsOpened = false;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}

