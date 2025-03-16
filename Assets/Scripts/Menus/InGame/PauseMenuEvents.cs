using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuEvents : MonoBehaviour
{
    private UIDocument document;

    private Button mainMenuButton;
    private Button continueButton;
    private Button restartButton;
    private Button settingsButton;
    private List<Button> menuButtons = new List<Button>();

    public PlayerController player;

    private string startMenuScene = "StartMenu";
    [Header("Name of current scene. CASE SENSITIVE!!")]
    public string currentScene;

    

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        mainMenuButton = document.rootVisualElement.Q("MainMenuButton") as Button;
        mainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuClick);

        continueButton = document.rootVisualElement.Q("ContinueButton") as Button;
        continueButton.RegisterCallback<ClickEvent>(OnContinueClick);
        
        restartButton = document.rootVisualElement.Q("RestartButton") as Button;
        restartButton.RegisterCallback<ClickEvent>(OnRestartClick);

        settingsButton = document.rootVisualElement.Q("SettingsButton") as Button;
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsClick);
        

        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnMainMenuClick(ClickEvent evt)
    {
        LoadScene(startMenuScene);
    }

    private void OnContinueClick(ClickEvent evt)
    {
        player.HandlePauseMenu();
    }

    private void OnRestartClick(ClickEvent evt)
    {
        LoadScene(currentScene);
    }

    private void OnSettingsClick(ClickEvent evt)
    {

    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        // TODO: If we want to add sounds or anything else that happens every time a button is clicked do it here
    }

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
