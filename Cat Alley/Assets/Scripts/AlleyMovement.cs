using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyMovement : MonoBehaviour
{
    private float alleySpeed;
    private GameObject _player;
    public Transform SpawnMarker;
    public Transform End;
    public Transform WaitingZone;
    public Transform StartWatingZone;
    public GameObject[] Cats;
    public GameObject[] OverHeadObstacles;
    public GameObject[] GroundObstacles;
    public Transform alleyTransform;
    public bool isTurnAlley = false;

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
        if(!gameObject.CompareTag("StartAlley")){ 
            foreach(GameObject obs in OverHeadObstacles){
                obs.SetActive(false);
            }
            foreach(GameObject obs in GroundObstacles){
                obs.SetActive(false);
            }
            foreach(GameObject cat in Cats){
                if(cat.activeSelf){
                    cat.GetComponent<CatScript>().State = "Idle";
                    cat.SetActive(false);
                }
            }
            transform.position = WaitingZone.position;
            GetComponent<AlleyMovement>().enabled = false;
        }
    }
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
            foreach(GameObject obs in OverHeadObstacles){
                obs.SetActive(false);
            }
            foreach(GameObject obs in GroundObstacles){
                obs.SetActive(false);
            }
            foreach(GameObject cat in Cats){
                if(cat.activeSelf){
                    cat.GetComponent<CatScript>().State = "Idle";
                    cat.SetActive(false);
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
