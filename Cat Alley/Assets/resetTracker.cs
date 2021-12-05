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


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider col){
        if(col.GetComponent<Collider>().name == "SpawnMarker"){
            this.spawn();
            Debug.Log("reset");
        }
        else if (col.GetComponent<Collider>().name == "DestroyMarker"){
            Debug.Log("destroy");
            GameObject.Destroy(this.transform.parent.gameObject);
        }
    }

    private void spawn()
    {

    
        //selecting which alley to reset
        var alleySelected = Random.Range(1, alleyNum);
        Debug.Log(alleySelected);

        //resetting the selected alley
        if (alleySelected == 1)
        {
            // alley1.GetComponent<AlleyMovement>().alleyReset();
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
