using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepScript : MonoBehaviour
{
    DogScript avoidThisDog;
    GoalPenScript thePen;

    Transform sheepPosition;
    float distanceFromDog;

    public enum StateOfSheep { captured = 0, free = 1, dead = 2 }
    public StateOfSheep sheepState;

    public float moveSpeed;
    public float retreatDistance;

    public delegate void CollectSheep(SheepScript sheep);
    public static event CollectSheep SheepCaptured;

    // Start is called before the first frame update
    void Start()
    {
        avoidThisDog = FindObjectOfType<DogScript>();
        thePen = FindObjectOfType<GoalPenScript>();
        sheepPosition = gameObject.transform;
        sheepState = StateOfSheep.free;
    }

    // Update is called once per frame
    void Update()
    {
        if (sheepState == StateOfSheep.free)
        {
            GetDistanceBetweenSheepAndDog();
            StayAwayFromDog();
        }
        else if (sheepState == StateOfSheep.captured)
        {
            MoveInPen();
        }
        else if (sheepState == StateOfSheep.dead)
        {
            Destroy(gameObject);
        }
    }


    void MoveInPen()
    {
        transform.position = Vector3.MoveTowards(transform.position, thePen.gatherPoint.position, (moveSpeed / 2f) * Time.deltaTime);
    }

    void StayAwayFromDog()
    {
        if (distanceFromDog < retreatDistance)
        {//to make the enemy retreat the speed is negative.
            transform.position = Vector3.MoveTowards(transform.position, avoidThisDog.gameObject.transform.position, -moveSpeed * Time.deltaTime);
        }
    }

    void GetDistanceBetweenSheepAndDog()
    {
        distanceFromDog = Vector3.Distance(avoidThisDog.gameObject.transform.position, sheepPosition.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;
        if (hit.CompareTag("GoalPen"))
        {
            sheepState = StateOfSheep.captured;
            SheepCaptured(this);
           
           
            
        }
        else if (hit.CompareTag("Enemy") || hit.CompareTag("Hazard"))
        {
            sheepState = StateOfSheep.dead;
        }
    }

}
