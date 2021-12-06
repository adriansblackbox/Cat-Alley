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
        }
        scoreTextValue = "Score: " + score;
        scoreText.text = scoreTextValue;

        

    }

    public bool gameOver(){
        if(lives <= 0){
            return true;
        } else{
            return false;
        }
    }
}
