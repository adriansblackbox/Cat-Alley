using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;
    public Text scoreText;
    public int cooldown;
    public int ammo;
    public int points;
    public Image heart1;
    public Image heart2;
    public Image heart3;

    private string scoreTextValue;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver()){
            //stop the game or something IDK
            Debug.Log("game over");
        }
        scoreTextValue = "Score: " + score;
        scoreText.text = scoreTextValue;

        this.checkHeart();

    }
    public void addScore(){
        score += points;
    }

    public void minusLive(){
        lives--;
    }

    public void checkHeart(){
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

    public bool gameOver(){
        if(lives <= 0){
            return true;
        } else{
            return false;
        }
    }
}
