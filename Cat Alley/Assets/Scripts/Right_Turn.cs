using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Turn : MonoBehaviour
{
    private bool turnRange = false;
    private bool turned = false;
    public float TurnSpeed = 200f;
    public bool isRightTurn = true;
    private void Update() {
        if((turnRange && Input.GetKey(KeyCode.D) && isRightTurn) || (turnRange && Input.GetKey(KeyCode.A) && !isRightTurn)){
            turned = true;
        }
        if(turned){
            if(isRightTurn)
                this.transform.rotation =  Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, -90f, 0f), Time.deltaTime * TurnSpeed);
            else
                this.transform.rotation =  Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 90f, 0f), Time.deltaTime * TurnSpeed);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            turnRange = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            turnRange = false;
            turned = false;
        }
    }
}
