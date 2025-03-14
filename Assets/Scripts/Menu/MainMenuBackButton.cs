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

    private void OnBackButtonClick(ClickEvent evt)
    {
        SceneManager.LoadScene(startMenuScene);

        // TODO: If we're adding clicking sounds when pressing buttons do that here
    }
}
