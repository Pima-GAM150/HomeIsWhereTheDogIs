using UnityEngine;
using UnityEngine.AI;

public class DogScript : MonoBehaviour
{
    public UIManager UI;
    public Camera cam;
    public NavMeshAgent agent;
    public int numberOfCommandsGiven;

    public SheepScript[] sheepToHerd;
    int numOfSheepToCatch;
    public int amountOfSheepCaught;

    public delegate void ClickToMove(DogScript movePoint);
    public static event ClickToMove ClicktoMovePoint;

    public delegate void CapturedAllSheep(DogScript dog);
    public static event CapturedAllSheep WonTheRound;

    void Start()
    {
        numberOfCommandsGiven = 0;
        amountOfSheepCaught = 0;
        sheepToHerd = FindObjectsOfType<SheepScript>();
        numOfSheepToCatch = sheepToHerd.Length;

        SheepScript.SheepCaptured += OnSheepCapture;
    }


    void Update()
    {
        MoveByFollowingTheMousePointer();
        UIUpdates();
        CheckSheepState();
    }

    void CheckSheepState()
    {
        if (amountOfSheepCaught == sheepToHerd.Length)
        {
            WonTheRound(this);
        }
    }

    void UIUpdates()
    {
        UI.numberOfClicksLabel.text = "Number of Commands " + numberOfCommandsGiven.ToString(); 
    }

    void OnSheepCapture(SheepScript sheepThatWasCaught)
    {
        amountOfSheepCaught++;
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
