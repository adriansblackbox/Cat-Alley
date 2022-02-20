using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetTracker : MonoBehaviour
{
    public List<GameObject> Alleys;
    private GameObject newAlley;
    private int alleySelected;
    public GameObject currentAlley;
    private List<int> randInts = new List<int>{0,0,0};

    public void Spawn(){
        //selecting which alley to reset
        alleySelected = Random.Range(0, Alleys.Count);
        Alleys[alleySelected].transform.position = currentAlley.GetComponent<AlleyMovement>().End.transform.position;
        Alleys[alleySelected].GetComponent<AlleyMovement>().enabled = true;
        if(Alleys[alleySelected].GetComponent<AlleyMovement>().Cats.Length >= 3){
            EnableCats(3);
        }else{
            // enable cats (how ever many cats are in turn alleys)
        }
        if(Alleys[alleySelected].GetComponent<AlleyMovement>().OverHeadObstacles.Length == 3){
            EnableObstacles(3);
        }else{
            // enable OBS (how ever many cats are in turn alleys)
        }
        //EnableObstacles(3);
        currentAlley = Alleys[alleySelected];
        Alleys.Remove(Alleys[alleySelected]);
    }
    private void EnableCats(int numCats){
        randInts.Clear();
        generateNums();
        for(int i = 0; i < numCats; i++){
           GameObject cat = Alleys[alleySelected].GetComponent<AlleyMovement>().Cats[randInts[i]];
           cat.SetActive(true);
           cat.GetComponent<CatScript>().enabled = true;
        }
    }
    private void EnableObstacles(int numObs){
        for (int i = 0; i < numObs; i++){
            int randInt = Random.Range(1, 3);
            if(randInt == 1){
                //spawn an overehad obs
                Alleys[alleySelected].GetComponent<AlleyMovement>().OverHeadObstacles[i].SetActive(true);
            }else{
                //spawn a ground obs
                Alleys[alleySelected].GetComponent<AlleyMovement>().GroundObstacles[i].SetActive(true);
            }
        }
    }
    private void generateNums(){
        for(int i = 0; i < 3; i++){
           int randNum = Random.Range(0, Alleys[alleySelected].GetComponent<AlleyMovement>().Cats.Length);
           if(randInts.Contains(randNum)){
               i--;
               continue;
           }
           randInts.Add(randNum);
        }
    }
}
