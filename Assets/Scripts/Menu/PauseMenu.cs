using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour, IOpenMenu
{
    [SerializeField]
    private GameObject pauseMenu;

    private bool isPause;

    void Start()
    {
        pauseMenu.SetActive(false);    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPause = !isPause;
        Time.timeScale = (!isPause ? 1 : 0);
        pauseMenu.SetActive(isPause);
    }

    public void OpenMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
