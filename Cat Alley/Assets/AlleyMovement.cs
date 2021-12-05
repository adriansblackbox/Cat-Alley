using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyMovement : MonoBehaviour
{
    //speed of the alley
    public int alleySpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var alley = this.GetComponent<Transform>();
        var alleyPosition = alley.position;

        //alley moves at constant speed
        alleyPosition.z += this.alleySpeed * Time.deltaTime;
        alley.position = alleyPosition;
    }

    public void alleyReset()
    {
        GameObject start = GameObject.Find("AlleyStartPosition");
        var startTransform = start.transform;
        var alleyPosition = startTransform.position;
        var alley = this.GetComponent<Transform>();
        alley.position = alleyPosition;
    } 
}
