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
    public GameObject PointText;
    private GameObject PointObject;
    public Transform PointStart, PointEnd;
    public bool PointMade = false;

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
        }else{
            handlePointAnimation();
        }
        if(catTransform.position.z >= playerTransform.position.z + 1f && Mathf.Abs(catTransform.position.x) <= playerTransform.position.x + 5){
            if(State == "Agro"){
                GameState.minusLive();
                GameState.Scratched = true;
            }
            Destroy(PointObject);
            this.enabled = false;
        }
        handleAnimation();
    }
    private void handlePointAnimation(){
        if(!PointMade){
            PointObject = GameObject.Instantiate(PointText, PointStart.position, Quaternion.identity);
            PointMade = true;
        }else if(PointObject.transform.position != PointEnd.position){
           PointObject.transform.position = Vector3.Lerp(PointObject.transform.position, PointEnd.position, Time.deltaTime * 10f);
        }
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
