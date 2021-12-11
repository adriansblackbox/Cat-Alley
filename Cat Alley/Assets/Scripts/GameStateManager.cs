using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public int lives = 4;
    public int score = 0;
    public Text scoreText;
    public int cooldown;
    public int ammo;
    public int points;
    public GameObject player;
    public GameObject gameOverMenu;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    public float AlleySpeed = 15f;
    private string scoreTextValue;
    private void Start() {
        FindObjectOfType<resetTracker>().Spawn();
        player.GetComponent<PlayerController>().enabled = true;
    }
    void Update()
    {
        if(lives <= 0){
            gameOver();
        } 
        scoreTextValue = "Score: " + score;
        scoreText.text = scoreTextValue;

        this.checkHeart();

       /* if(Input.GetKeyDown(KeyCode.Escape))
        Application.Quit();
       */
    }
    public void addScore(){
        score += points;
    }

    public void minusLive(){
        lives--;
    }

    public void checkHeart(){
        if (lives == 3){
            heart4.enabled = false;
        }
        if (lives == 2){
            heart3.enabled = false;
        }
        if (lives == 1){
            heart2.enabled = false;
        }
        if (lives == 0){
            heart1.enabled = false;
        }
    }

    public void restart(){
        SceneManager.LoadScene("prototype");
    }

    public void gameOver(){
        player.GetComponent<PlayerController>().enabled = false;
        gameOverMenu.SetActive(true);
    }
}
