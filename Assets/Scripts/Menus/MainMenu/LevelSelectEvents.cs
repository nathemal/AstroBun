using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSelectEvents : MonoBehaviour
{
    private UIDocument document;

    private Button tutorialButton;
    private Button levelOneButton;
    private Button levelTwoButton;
    private Button levelThreeButton;
    private List<Button> menuButtons = new List<Button>();

    private string tutorialScene = "Tutorial";
    private string levelOneScene = "LevelOne";
    private string levelTwoScene = "LevelTwo";
    private string levelThreeScene = "LevelThree";

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        tutorialButton = document.rootVisualElement.Q("TutorialButton") as Button;
        tutorialButton.RegisterCallback<ClickEvent>(OnTutorialClick);

        levelOneButton = document.rootVisualElement.Q("LevelOneButton") as Button;
        levelOneButton.RegisterCallback<ClickEvent>(OnLevelOneClick);

        levelTwoButton = document.rootVisualElement.Q("LevelTwoButton") as Button;
        levelTwoButton.RegisterCallback<ClickEvent>(OnLevelTwoClick);

        levelThreeButton = document.rootVisualElement.Q("LevelThreeButton") as Button;
        levelThreeButton.RegisterCallback<ClickEvent>(OnLevelThreeClick);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnTutorialClick(ClickEvent evt)
    {
        ResetGameData();
        LoadScene(tutorialScene);
    }

    private void OnLevelOneClick(ClickEvent evt)
    {
        ResetGameData();
        LoadScene(levelOneScene);
    }

    private void OnLevelTwoClick(ClickEvent evt)
    {
        ResetGameData();
        LoadScene(levelTwoScene);
    }

    private void OnLevelThreeClick(ClickEvent evt)
    {
        ResetGameData();
        LoadScene(levelThreeScene);
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        // TODO: If we want to add sounds or anything else that happens every time a button is clicked do it here
    }

    private void LoadScene(string scene)
    {
        Time.timeScale = 1f; //unfreeze everything
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