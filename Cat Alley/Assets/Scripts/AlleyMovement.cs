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

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(End.position.z > _player.transform.position.z){
            alleyTransform.eulerAngles = new Vector3(0f,0f,0f);
            Sleep();
        }
    }
    private void Sleep(){
        if(!gameObject.CompareTag("StartAlley")){ 
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
