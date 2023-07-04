using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelSelect() {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadGameLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }
}
