using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathMenuEvents : MonoBehaviour
{
    private UIDocument document;

    private Button mainMenuButton;
    private Button retryButton;
    private Button exitGameButton;
    private List<Button> menuButtons = new List<Button>();

    public PlayerController player;

    private string levelSelectScene = "LevelSelect";
    [Header("Name of current scene. CASE SENSITIVE!!")]
    public string currentScene;
   

    private void Update()
    {
        document = GetComponent<UIDocument>();

        mainMenuButton = document.rootVisualElement.Q("LevelSelectButton") as Button;
        mainMenuButton.RegisterCallback<ClickEvent>(OnLevelSelectClick);

        retryButton = document.rootVisualElement.Q("RetryButton") as Button;
        retryButton.RegisterCallback<ClickEvent>(OnRestartClick);

        exitGameButton = document.rootVisualElement.Q("ExitGameButton") as Button;
        exitGameButton.RegisterCallback<ClickEvent>(OnExitClick);


        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnLevelSelectClick(ClickEvent evt)
    {
        ResetGameData();
        LoadScene(levelSelectScene);
    }

    private void OnRestartClick(ClickEvent evt)
    {
        Time.timeScale = 1;

        ResetGameData();
		
        LoadScene(currentScene);
    }

    private void OnExitClick(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        // TODO: If we want to add sounds or anything else that happens every time a button is clicked do it here
    }

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void ResetGameData()
    {
        if (AllEntityDataManager.Instance != null)
        {
            AllEntityDataManager.Instance.ResetPlayerData();
            AllEntityDataManager.Instance.ResetAllEnemyData();
        }
    }
}
