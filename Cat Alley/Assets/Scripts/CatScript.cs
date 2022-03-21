using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    public GameStateManager GameState;
    public string State;
    public float AgroDistance = 10f;
    private Transform catTransform;
    private Transform playerTransform;
    private SpriteRenderer catSR;
    public Sprite catIdle, catAgro, catSatisfied;

    void Start()
    {
        State = "Idle";
        catTransform = this.GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        catSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(State != "Satisfied"){
            if(catTransform.position.z >= playerTransform.position.z  - AgroDistance){
                State = "Agro";
            }else{
                State = "Idle";
            }
        }
        if(catTransform.position.z >= playerTransform.position.z + 2f){
            if(State == "Agro"){
                GameState.minusLive();
            }
            this.enabled = false;
        }
        handleAnimation();
    }
    public void handleAnimation(){
        switch(State){
            case "Idle":
            catSR.sprite = catIdle;
            break;
            case "Agro":
            catSR.sprite = catAgro;
            break;
            case "Satisfied":
            catSR.sprite = catSatisfied;
            break;
        }
    }
}
