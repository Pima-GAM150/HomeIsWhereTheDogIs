using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSelectionScript : MonoBehaviour
{

    int levelToLoad;
    Scene currentLevel;

    int level1 = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FadeToLevel (int levelIndex)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    void ReplayCurrentLevel()
    {

    }

    void NextLevel()
    {

    }

    void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }


}
