using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameStateManager : MonoBehaviour
{
    public int lives = 2;
    public int score = 0;
    public Text scoreText;
    public Text highScoreText;
    public int points;
    public GameObject player;
    public GameObject gameOverMenu;
    public GameObject canvas;
    public float AlleySpeed = 15f;
    public float maxSpeed;
    public bool inGame;
    public float speedAdd;
    private string scoreTextValue;
    private float time;
    private bool regeningHealth = false;
    public GameObject postProcessing;
    private Vignette vignette;
    public Text MouseSensitivityText1, MouseSensitivityText2;
    public Slider MouseSensitivitySlider1, MouseSensitivitySlider2;
    private float highScore = 0;
    public Text HighScoreBoard;
    public bool Scratched = false;
    public GameObject Scratch;
    public bool GameOver;
    

    private void Start() {
        /*
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            //Set first time opening to false
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
            //Do your stuff here
            PlayerPrefs.DeleteAll();
        }
        */
        highScore = PlayerPrefs.GetFloat("High Score");
        HighScoreBoard.text = "Best Score: " + highScore.ToString();
        FindObjectOfType<resetTracker>().Spawn();
        player.GetComponent<PlayerController>().enabled = false;
        FindObjectOfType<GunMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inGame = false;
        postProcessing.GetComponent<Volume>().profile.TryGet(out vignette);
        // Sets saved mosue sensitivity
        MouseSensitivitySlider1.value = PlayerPrefs.GetFloat("Mouse Sensitivity");
        MouseSensitivityText1.text =  MouseSensitivitySlider1.value.ToString();
        Scratch.SetActive(false);
        GameOver = false;
    }
    void Update()
    {
        if(lives <= 0){
            gameOver();
        }else if(lives < 2 && !regeningHealth && !GameOver){
            StartCoroutine(regenHealth());
        }
        // handles score counting
        scoreTextValue = "Score: " + score;
        scoreText.text = scoreTextValue;
         if(score >= highScore){
            highScore = score;
            PlayerPrefs.SetFloat("High Score", highScore);
        }
        highScoreText.text = "Best: " + highScore.ToString();
        // handles difficulty over time
        if (inGame == true)
        {
            this.addSpeed();
        }
    }
    public void addScore(){
        score += points;
    }

    public void minusLive(){
        lives--;
    }

    public void addSpeed(){
        if (!FindObjectOfType<MainMenuScript>().isPaused && AlleySpeed < maxSpeed){
            time+= Time.deltaTime;
            if(time>10){
                AlleySpeed += speedAdd;
                time = 0;
            }
        }
    }

    public void restart(){
        SceneManager.LoadScene("prototype");
    }

    public void gameOver(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<PlayerController>().enabled = false;
        gameOverMenu.SetActive(true);
        Scratched = false;
        Scratch.SetActive(false);
        vignette.intensity.value = 0f;
        PlayerPrefs.Save();
        FindObjectOfType<GunMovement>().enabled = false;
        GameOver = true;
    }
    private IEnumerator regenHealth(){
        regeningHealth = true;
        vignette.intensity.value = 0.5f;
        if(Scratched)
            Scratch.SetActive(true);
        yield return new WaitForSeconds(5f);
            vignette.intensity.value = 0f;
            lives += 1;
            regeningHealth = false;
            if(Scratched){
                Scratched = false;
                Scratch.SetActive(false);
            }
        yield return null;
    }
    public void OnMouseSensitivityChange(){
        if(!FindObjectOfType<MainMenuScript>().isPaused) {
            PlayerPrefs.SetFloat("Mouse Sensitivity", MouseSensitivitySlider1.value);
            MouseSensitivitySlider2.value = MouseSensitivitySlider1.value;
        }else
            PlayerPrefs.SetFloat("Mouse Sensitivity", MouseSensitivitySlider2.value);
        MouseSensitivityText1.text =  MouseSensitivitySlider1.value.ToString();
        MouseSensitivityText2.text =  MouseSensitivitySlider2.value.ToString();
        PlayerPrefs.Save();
    }
}
