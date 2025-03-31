using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// TODO: Add a hover state for the buttons in the start menu and a transition between the 2 states

public class StartMenuEvents : MonoBehaviour
{
    private UIDocument document;

    private Button startButton;
    private Button settingsButton;
    private Button creditsButton;
    private Button exitGameButton;
    private List<Button> menuButtons = new List<Button>();

    private string levelSelectScene = "LevelSelect";
    private string settingsScene = "Settings";
    private string creditsScene = "Credits";

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        startButton = document.rootVisualElement.Q("StartGameButton") as Button;
        startButton.RegisterCallback<ClickEvent>(OnStartGameClick);

        settingsButton = document.rootVisualElement.Q("SettingsButton") as Button;
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsClick);

        creditsButton = document.rootVisualElement.Q("CreditsButton") as Button;
        creditsButton.RegisterCallback<ClickEvent>(OnCreditsClick);

        exitGameButton = document.rootVisualElement.Q("QuitButton") as Button;
        exitGameButton.RegisterCallback<ClickEvent>(OnExitGameClick);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnStartGameClick(ClickEvent evt)
    {
        ResetGameData();
        LoadScene(levelSelectScene);
    }

    private void OnSettingsClick(ClickEvent evt)
    {
        //LoadScene(settingsScene);
    }

    private void OnCreditsClick(ClickEvent evt)
    {
        LoadScene(creditsScene);
    }

    private void OnExitGameClick(ClickEvent evt)
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