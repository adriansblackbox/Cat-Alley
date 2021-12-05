using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetTracker : MonoBehaviour
{
    public GameObject alley1;
    public GameObject alley2;
    public GameObject alley3;

    public int resetSpeed = 10;

    private GameObject temp;
    private Transform startTransform;
    // Start is called before the first frame update
    void Start()
    {
        temp = GameObject.Find("StartPosition");
        startTransform = temp.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var resetTransform = this.GetComponent<Transform>();
        var resetPosition = resetTransform.position;

        resetPosition.z += this.resetSpeed * Time.deltaTime;
        resetTransform.position = resetPosition;

        this.positionCheck();
    }

    private void positionCheck()
    {
    var mark = GameObject.Find("ResetMarker");
    var markTransform = mark.transform;
        var resetTransform = this.GetComponent<Transform>();
    var resetPosition = resetTransform.position;
        if (resetPosition.z > markTransform.position.z) {
            this.loopReset();
        }
}

    private void loopReset()
    {

    
        //selecting which alley to reset
        var alleySelected = Random.Range(1, 4);

        //resetting the selected alley
        if (alleySelected == 1)
        {
            alley1.GetComponent<AlleyMovement>().alleyReset();
            Debug.Log("resetting 1");
        }
        else if (alleySelected == 2)
        {
            alley2.GetComponent<AlleyMovement>().alleyReset();
        }
        else if (alleySelected == 3)
        {
            alley3.GetComponent<AlleyMovement>().alleyReset();
        }

        //resetting the reseter
        var resetTransform = this.GetComponent<Transform>();
        resetTransform.position = startTransform.position; 
    }
}
