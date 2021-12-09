using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyMovement : MonoBehaviour
{
    private float alleySpeed;
    private Transform alley;
    public Transform SpawnMarker;
    public Transform End;
    public Transform WaitingZone;
    public Transform StartWatingZone;
    private void Start() {
        alley = this.GetComponent<Transform>();
    }
    void Update()
    {
        if(End.position.z >= SpawnMarker.position.z) Sleep();

        alleySpeed = FindObjectOfType<GameStateManager>().AlleySpeed;
        //alley moves at constant speed
        transform.position += new Vector3 (0.0f, 0.0f, this.alleySpeed * Time.deltaTime);
    }
    private void Sleep(){
        if(gameObject.CompareTag("StartAlley")){ 
            FindObjectOfType<resetTracker>().Spawn();
            transform.position = StartWatingZone.position;
            GetComponent<AlleyMovement>().enabled = false;
        }else{
            transform.position = WaitingZone.position;
            FindObjectOfType<resetTracker>().Alleys.Add(this.gameObject);
            FindObjectOfType<resetTracker>().Spawn();
            GetComponent<AlleyMovement>().enabled = false;
        }
    }
}
