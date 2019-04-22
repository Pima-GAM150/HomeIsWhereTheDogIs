using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;

public class SheepScript : MonoBehaviour
{
    List<Foliage> eats = new List<Foliage>();
    DogScript avoidThisDog;
    FoxScript avoidThisFox;
    GoalPenScript thePen;

    NavMeshAgent sheep;
    Transform sheepPosition;
    float distanceFromDog;
    float distanceFromFox;

    public enum StateOfSheep { captured = 0, free = 1, dead = 2 }
    public StateOfSheep sheepState;

    public float moveSpeed;
    public float retreatDistance;
    public float herdingDistance;

    public delegate void CollectSheep(SheepScript sheep);
    public static event CollectSheep SheepCaptured;


    private void Awake()
    {
        sheep = GetComponent<NavMeshAgent>();
    }



    void Start()
    {
        avoidThisDog = FindObjectOfType<DogScript>();
        thePen = FindObjectOfType<GoalPenScript>();
        eats.AddRange(FindObjectsOfType<Foliage>());

        avoidThisFox = FindObjectOfType<FoxScript>();
        sheepPosition = gameObject.transform;
        sheepState = StateOfSheep.free;
    }

    // Update is called once per frame
    void Update()
    {
        if (sheepState == StateOfSheep.free)
        {
            GetDistanceBetweenSheepAndDogAndFox();
            BeingHerdByDog();
            if (avoidThisFox != null)
                StayAwayFromFox();
            if (distanceFromFox > retreatDistance)
                SafeToEat();

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

    void SafeToEat()
    {
        Foliage[] options = eats.ToArray();
        int rng = Random.Range(0, (options.Length-1));

         sheep.SetDestination(options[rng].transform.position);
        
    }

    void MoveInPen()
    {
        transform.position = Vector3.MoveTowards(transform.position, thePen.gatherPoint.position, (moveSpeed / 2f) * Time.deltaTime);
    }

    void BeingHerdByDog()
    {
        if (distanceFromDog < herdingDistance)
        {
            // transform.position = Vector3.MoveTowards(transform.position, thePen.transform.position, moveSpeed * Time.deltaTime);
            sheep.SetDestination(thePen.transform.position);

        }
    }

    void StayAwayFromFox()
    {
        if (distanceFromFox < retreatDistance)
        {//to make the enemy retreat the speed is negative.
            //transform.position = Vector3.MoveTowards(transform.position, avoidThisFox.gameObject.transform.position, -moveSpeed * Time.deltaTime);
            sheep.SetDestination(RandomRetreat());
        }
    }

    public Vector3 RandomRetreat()
    {
        Vector3 retreatTo = new Vector3();
        float timer = 2f;
        int run = (int)retreatDistance;
        int rngwhere = Random.Range(1, 2);

        if (avoidThisFox.transform.position.x > transform.position.x && rngwhere == 1)
        {            
            retreatTo = new Vector3(gameObject.transform.position.x - run, gameObject.transform.position.y, gameObject.transform.position.z);
            timer -= Time.deltaTime;
        }
        if (avoidThisFox.transform.position.x < transform.position.x && rngwhere == 2)
        {
            retreatTo = new Vector3(gameObject.transform.position.x + run, gameObject.transform.position.y, gameObject.transform.position.z);
            timer -= Time.deltaTime;
        }
        if (avoidThisFox.transform.position.z > transform.position.z && rngwhere == 2)
        {
            retreatTo = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - run);
            timer -= Time.deltaTime;
        }
        if (avoidThisFox.transform.position.z < transform.position.z && rngwhere == 1)
        {
            retreatTo = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + run);
            timer -= Time.deltaTime;
        }

        return retreatTo;
    }

    void GetDistanceBetweenSheepAndDogAndFox()
    {
        distanceFromDog = Vector3.Distance(avoidThisDog.gameObject.transform.position, sheepPosition.transform.position);
        if (avoidThisFox != null)
            distanceFromFox = Vector3.Distance(avoidThisFox.gameObject.transform.position, sheepPosition.transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        
        GameObject hit = other.gameObject;
        Debug.Log("Touching " + hit.tag);
        if (hit.CompareTag("GoalPen"))
        {
            Debug.Log("GOOOAALL");
            sheepState = StateOfSheep.captured;
            SheepCaptured(this);
           
           
            
        }
        else if (hit.CompareTag("Enemy") || hit.CompareTag("Hazard"))
        {
            sheepState = StateOfSheep.dead;
        }
    }

}
