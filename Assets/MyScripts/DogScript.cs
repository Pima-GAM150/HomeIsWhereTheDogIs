using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class DogScript : MonoBehaviour
{
    public UIManager UI;
    public Camera cam;
    public NavMeshAgent agent;
    public int numberOfCommandsGiven;

    public List<SheepScript> sheepToHerd = new List<SheepScript>();
    public int numOfSheepToCatch;
    public int amountOfSheepCaught;

    public delegate void ClickToMove(DogScript movePoint);
    public static event ClickToMove ClicktoMovePoint;

    public delegate void CapturedAllSheep(DogScript dog);
    public static event CapturedAllSheep WonTheRound;

    

    private void Awake()
    {
        
        UI = FindObjectOfType<UIManager>();
        sheepToHerd.AddRange(FindObjectsOfType<SheepScript>());
    }

    void Start()
    {
        numberOfCommandsGiven = 0;
        amountOfSheepCaught = 0;
        numOfSheepToCatch = sheepToHerd.Count;
        SheepScript.SheepCaptured += OnSheepCapture;
        SheepScript.SheepDied += OnSheepDeath;
    }


    void Update()
    {
        MoveByFollowingTheMousePointer();
        UIUpdates();
        CheckSheepState();
    }

    void CheckSheepState()
    {
        if (amountOfSheepCaught >= sheepToHerd.Count)
        {
            UI.endScreen.gameObject.SetActive(true);
        }
    }

    void UIUpdates()
    {
        if (UI != null)
        {
            UI.numberOfClicksLabel.text = "Number of Commands: " + numberOfCommandsGiven.ToString();
            UI.sheepCollectedLabel.text = "Sheep Gathered: " + amountOfSheepCaught.ToString();
        }
    }

    void OnSheepCapture(SheepScript sheepThatWasCaught)
    {
        amountOfSheepCaught++;
    }

    void OnSheepDeath(SheepScript sheepThatDied)
    {
        sheepToHerd.Remove(sheepThatDied);
   }

    void MoveByFollowingTheMousePointer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {//move agent
                agent.SetDestination(hit.point);
                numberOfCommandsGiven++;
                
            }

        }
    }

}
