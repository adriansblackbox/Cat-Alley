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
    public Text MouseSensitivityText;
    public Slider MouseSensitivitySlider;
    private float highScore = 0;
    

    private void Start() {
        highScore = PlayerPrefs.GetFloat("High Score");
        FindObjectOfType<resetTracker>().Spawn();
        player.GetComponent<PlayerController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inGame = false;
        postProcessing.GetComponent<Volume>().profile.TryGet(out vignette);
        // Sets saved mosue sensitivity
        MouseSensitivitySlider.value = PlayerPrefs.GetFloat("Mouse Sensitivity");
        MouseSensitivityText.text =  MouseSensitivitySlider.value.ToString();
    }
    void Update()
    {
        if(lives <= 0){
            gameOver();
        }else if(lives < 2 && !regeningHealth){
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
        if (canvas.GetComponent<MenuScript>().isPaused && AlleySpeed < maxSpeed){
        } else{
            time+= Time.deltaTime;
            if(time>10){
                AlleySpeed += speedAdd;
                Debug.Log("speed" + AlleySpeed);
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
        PlayerPrefs.Save();
    }
    private IEnumerator regenHealth(){
        regeningHealth = true;
        vignette.intensity.value = 0.5f;
        Debug.Log("OWW");
        yield return new WaitForSeconds(5f);
        if(lives > 0){
            vignette.intensity.value = 0f;
            lives += 1;
            regeningHealth = false;
        }else{
            // Game Over Case
        }
        Debug.Log("Okay");
        yield return null;
    }
     public void OnMouseSensitivityChange(){
        PlayerPrefs.SetFloat("Mouse Sensitivity", MouseSensitivitySlider.value);
        MouseSensitivityText.text =  MouseSensitivitySlider.value.ToString();
        PlayerPrefs.Save();
    }
}
