using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas playScreen;
    public Canvas endScreen;
    public Text sheepCollectedLabel;
    public Text numberOfClicksLabel;

    

    void Start()
    {
        DogScript.WonTheRound += OnRoundEnd;
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnRoundEnd(DogScript dog)
    {
        endScreen.gameObject.SetActive(true);
    }
    
}
