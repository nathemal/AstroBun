using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public bool clickedMainMenu = false;
    public bool clickedStartGame = false;
    public bool clickedOptions = false;
    public bool clickedCredits = false;
    public bool clickedQuit = false;

    private string levelName;

    private void Update()
    {
        if (clickedMainMenu)
        {
            clickedMainMenu = false;
            levelName = "MainMenu";
            SceneManager.LoadScene(levelName);
        }

        if (clickedStartGame)
        {
            clickedStartGame = false;
            levelName = "LevelSelect";
            SceneManager.LoadScene(levelName);
        }

        if (clickedOptions)
        {
            clickedOptions = false;
            levelName = "Options";
            SceneManager.LoadScene(levelName);
        }

        if (clickedCredits)
        {
            clickedCredits = false;
            levelName = "MainMenu";
            SceneManager.LoadScene(levelName);
        }
    }
}