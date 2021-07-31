using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
