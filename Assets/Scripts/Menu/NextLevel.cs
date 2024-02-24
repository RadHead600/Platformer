using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour, IOpenMenu
{
    public void OpenNextLevel()
    {
        SaveParameters.levelActive++;
        if (SceneManager.sceneCountInBuildSettings > SaveParameters.levelActive)
        {
            SceneManager.LoadScene(SaveParameters.levelActive);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OpenMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
