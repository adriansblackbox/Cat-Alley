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
        if(Alleys[alleySelected].GetComponent<AlleyMovement>().Cats.Length > 0)
            EnableCats();
        //EnableObstacles();
        currentAlley = Alleys[alleySelected];
        Alleys.Remove(Alleys[alleySelected]);
    }
    private void EnableCats(){
        randInts.Clear();
        generateNums();
        foreach(int num in randInts){
            Debug.Log(num);
        }
        for(int i = 0; i < 3; i++){
           GameObject cat = Alleys[alleySelected].GetComponent<AlleyMovement>().Cats[randInts[i]];
           cat.SetActive(true);
           cat.GetComponent<CatScript>().enabled = true;
        }
    }
    private void EnableObstacles(){
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
