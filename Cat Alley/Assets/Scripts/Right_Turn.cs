using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Turn : MonoBehaviour
{
    private bool turnRange = false;
    public bool turned = false;
    public float TurnSpeed = 200f;
    public bool isRightTurn = true;
    public GameObject Arrow, Wall;
    public Sprite GreenArrow, RedArrow;
    private void Update() {
        if((turnRange && Input.GetKeyDown(KeyCode.D) && isRightTurn) || (turnRange && Input.GetKeyDown(KeyCode.A) && !isRightTurn)){
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
            Arrow.GetComponent<SpriteRenderer>().sprite = GreenArrow;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            Arrow.GetComponent<SpriteRenderer>().sprite = RedArrow;
            turnRange = false;
            turned = false;
        }
    }
}
