using UnityEngine;
using UnityEngine.SceneManagement;

// Put this on the "Next Level" button (or any object) and wire the button's OnClick to LoadNextLevel().
public class LevelLoader : MonoBehaviour
{
    [Tooltip("Name of the scene to load. It must be listed in Build Settings.")]
    [SerializeField] string nextSceneName = "level_2";

    public void LoadNextLevel()
    {
        // ScoreManager pauses the game when the level is complete, so unpause before loading
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }
}
