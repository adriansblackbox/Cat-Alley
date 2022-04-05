using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyMovement : MonoBehaviour
{
    private float alleySpeed;
    public GameObject _player;
    public Transform SpawnMarker;
    public Transform End;
    public Transform WaitingZone;
    public Transform StartWatingZone;
    public GameObject[] Cats;
    public GameObject[] OverHeadObstacles;
    public GameObject[] GroundObstacles;
    public Transform alleyTransform;
    public bool isTurnAlley = false;

    void Update()
    {
        if(End.position.z > _player.transform.position.z + 20f){
            alleyTransform.eulerAngles = new Vector3(0f,0f,0f);
            if(isTurnAlley){
                this.gameObject.GetComponentInChildren<Right_Turn>().ResetTurn();
            }
            Sleep();
        }
    }
    private void Sleep(){
        if(!gameObject.CompareTag("StartAlley")){ 
            for(int i = 0; i < OverHeadObstacles.Length; ++i){
                OverHeadObstacles[i].SetActive(false);
            }
            for(int i = 0; i <  GroundObstacles.Length; ++i){
                GroundObstacles[i].SetActive(false);
            }
            for(int i = 0; i < Cats.Length; ++i){
                if(Cats[i].activeSelf){
                    Cats[i].GetComponent<CatScript>().State = "Idle";
                    Cats[i].SetActive(false);
                }
            }
            transform.position = WaitingZone.position;
            FindObjectOfType<resetTracker>().Alleys.Add(this.gameObject);
            GetComponent<AlleyMovement>().enabled = false;
            FindObjectOfType<resetTracker>().Spawn();
        }else{
            FindObjectOfType<resetTracker>().Spawn();
            transform.position = StartWatingZone.position;
            GetComponent<AlleyMovement>().enabled = false;
        }
    }
}
