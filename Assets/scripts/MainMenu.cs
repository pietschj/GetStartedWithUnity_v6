using UnityEngine;
using UnityEngine.SceneManagement;

// Attach to any object in the menu scene and wire the Play button's OnClick to PlayGame().
public class MainMenu : MonoBehaviour
{
    [Tooltip("Name of the scene to load when Play is pressed. It must be listed in Build Settings.")]
    public string gameSceneName = "level_1";

    public void PlayGame()
    {
        Debug.Log("starting game");
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        //a test to check that we are playing inside the editor
        //because we can't quite the game inside the editor
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
