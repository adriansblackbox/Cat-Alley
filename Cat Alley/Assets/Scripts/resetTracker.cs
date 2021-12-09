using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetTracker : MonoBehaviour
{
    public List<GameObject> Alleys;
    private GameObject newAlley;

    public void Spawn(){
        //selecting which alley to reset
        int alleySelected = Random.Range(0, Alleys.Count);
        //Alleys[alleySelected]
        //newAlley = Instantiate(Alleys[alleySelected]);

        GameObject start = GameObject.Find("AlleyStartPosition");
        var startPosition = start.transform.position;
        var alleyTransform = newAlley.GetComponent<Transform>();
        alleyTransform.position = startPosition;
        var movement = newAlley.GetComponent<AlleyMovement>();
        movement.enabled = true;
    }
}
