﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxScript : MonoBehaviour
{
    NavMeshAgent fox;
    DogScript dog;
    SheepScript[] sheepToEat;
    Transform currentSheepTarget;
    public Transform foxHole;

    float distanceFromDog;
    public float retreatDistance;


    void Start()
    {
        dog = FindObjectOfType<DogScript>();
        fox = GetComponent<NavMeshAgent>();
        sheepToEat = FindObjectsOfType<SheepScript>();
    }

    // Update is called once per frame
    void Update()
    {

        ChaseAfterSheepToEat();
        StayAwayFromDog();


    }

    void StayAwayFromDog()
    {
        distanceFromDog = Vector3.Distance(gameObject.transform.position, dog.gameObject.transform.position);
        if (distanceFromDog < retreatDistance)
        {
            fox.SetDestination(foxHole.position);
        }
    }

   void ChaseAfterSheepToEat()
    {
        if (currentSheepTarget == null || currentSheepTarget.GetComponent<SheepScript>().sheepState == SheepScript.StateOfSheep.captured)
        {
            int rng;
            rng = Random.Range(0, sheepToEat.Length);
            currentSheepTarget = sheepToEat[rng].gameObject.transform;
        }
        else
        {
            fox.SetDestination(currentSheepTarget.position);
        }
    }
}
