using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetTracker : MonoBehaviour
{
    public GameObject alley1;
    public GameObject alley2;
    public GameObject alley3;

    private GameObject newAlley;

    public int alleyNum; 
    public int resetSpeed = 10;

    private void OnTriggerEnter(Collider col){
        if(col.GetComponent<Collider>().name == "SpawnMarker"){
            this.spawn();
        }
        else if (col.GetComponent<Collider>().name == "DestroyMarker"){
            GameObject.Destroy(this.transform.parent.gameObject);
        }
    }

    private void spawn()
    {

    
        //selecting which alley to reset
        var alleySelected = Random.Range(1, alleyNum);

        //resetting the selected alley
        if (alleySelected == 1)
        {
            newAlley = Instantiate(alley1);
        }
        else if (alleySelected == 2)
        {
            newAlley = Instantiate(alley2);
        }
        else if (alleySelected == 3)
        {
            newAlley = Instantiate(alley3);
        }

        GameObject start = GameObject.Find("AlleyStartPosition");
        var startPosition = start.transform.position;
        var alleyTransform = newAlley.GetComponent<Transform>();
        alleyTransform.position = startPosition;
        var movement = newAlley.GetComponent<AlleyMovement>();
        movement.enabled = true;
    }
}
