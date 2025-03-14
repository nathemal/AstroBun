using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string levelName;

    public void _LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}