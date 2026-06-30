using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Custom Methods
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
