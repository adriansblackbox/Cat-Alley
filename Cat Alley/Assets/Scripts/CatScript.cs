using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    public GameStateManager state;
    private Transform catTransform;
    private Transform playerTransform;

    void Start()
    {
        catTransform = this.GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(catTransform.position.z >= playerTransform.position.z){
            Debug.Log("MEOOOOWWW");
            state.minusLive();
            this.enabled = false;
        }
    }
}
