using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   // public static UIManager instance;

    public Canvas playScreen;
    public Canvas endScreen;
    public Text sheepCollectedLabel;
    public Text numberOfClicksLabel;

    private void Awake()
    {
        //if (instance == null)
        //    instance = this;
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //DontDestroyOnLoad(instance);
        //DogScript.WonTheRound += OnRoundEnd;
    }

    void Start()
    {
        
        endScreen.gameObject.SetActive(false);
    }

    private void OnRenderObject()
    {

        //endScreen.gameObject.SetActive(false);
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
