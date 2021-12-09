using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyMovement : MonoBehaviour
{
    public int alleySpeed = 20;
    Transform alley;
    private void Start() {
        alley = this.GetComponent<Transform>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        var alleyPosition = alley.position;
        //alley moves at constant speed
        alleyPosition.z += this.alleySpeed * Time.deltaTime;
        alley.position = alleyPosition;
    }
}
