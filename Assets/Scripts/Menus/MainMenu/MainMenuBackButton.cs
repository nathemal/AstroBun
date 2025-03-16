using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuBackButton : MonoBehaviour
{
    private UIDocument document;

    private Button mainMenuBackButton;

    private string startMenuScene = "StartMenu";
    
    private void Awake()
    {
        document = GetComponent<UIDocument>();

        mainMenuBackButton = document.rootVisualElement.Q("MainMenuBackButton") as Button;
        mainMenuBackButton.RegisterCallback<ClickEvent>(OnBackButtonClick);
    }

    private void Update()
    {
        HandleInput();
    }

    private void OnBackButtonClick(ClickEvent evt)
    {
        LoadScene(startMenuScene);

        // TODO: If we're adding clicking sounds when pressing buttons do that here
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScene(startMenuScene);
        }
    }

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}