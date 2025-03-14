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

        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnStartGameClick(ClickEvent evt)
    {
        SceneManager.LoadScene(levelSelectScene);
    }

    private void OnSettingsClick(ClickEvent evt)
    {
        SceneManager.LoadScene(settingsScene);
    }

    private void OnCreditsClick(ClickEvent evt)
    {
        SceneManager.LoadScene(creditsScene);
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        // TODO: If we want to add sounds or anything else that happens every time a button is clicked do it here
    }
}