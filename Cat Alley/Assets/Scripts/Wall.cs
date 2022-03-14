using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject turnAlley;
    private void OnTriggerEnter(Collider other) {
        if(!turnAlley.GetComponent<Right_Turn>().turned)
            if(other.gameObject.tag == "Player"){
                FindObjectOfType<GameStateManager>().gameOver();
            }
    }
}
