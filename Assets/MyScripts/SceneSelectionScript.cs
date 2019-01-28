using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSelectionScript : MonoBehaviour
{

    int levelToLoad;
    Scene currentLevel;

    int level1 = 1;

    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel (int levelIndex)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReplayCurrentLevel()
    {
        currentLevel = SceneManager.GetActiveScene();
        int currentLVl = currentLevel.buildIndex;
        SceneManager.LoadScene(currentLVl);
    }

    public void NextLevel()
    {
        currentLevel = SceneManager.GetActiveScene();
        int nextlvl = currentLevel.buildIndex;
        nextlvl++;
        SceneManager.LoadScene(nextlvl);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }


}
