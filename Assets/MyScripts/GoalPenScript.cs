using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPenScript : MonoBehaviour
{
    public int numOfSheep;
    public Transform gatherPoint;

    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSheepColleted(SheepScript sheepThatWasCollected)
    {
        numOfSheep++;
    }
}
