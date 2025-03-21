using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathMenuEvents : MonoBehaviour
{
    private UIDocument document;

    private Button mainMenuButton;
    private Button retryButton;
    private List<Button> menuButtons = new List<Button>();

    private string startMenuScene = "StartMenu";
    [Header("Name of current scene. CASE SENSITIVE!!")]
    public string currentScene;


    //[Header("For carrying over data to next level")]
    //[SerializeField] public List<EnemyData> EnemiesDatasList;
    //[SerializeField] public PlayerData playerData;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        mainMenuButton = document.rootVisualElement.Q("MainMenuButton") as Button;
        mainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuClick);

        retryButton = document.rootVisualElement.Q("RetryButton") as Button;
        retryButton.RegisterCallback<ClickEvent>(OnRetryClick);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnMainMenuClick(ClickEvent evt)
    {
        ResetGameData();
        LoadScene(startMenuScene);
    }

    private void OnRetryClick(ClickEvent evt)
    {
        ResetGameData();
        LoadScene(currentScene);
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
