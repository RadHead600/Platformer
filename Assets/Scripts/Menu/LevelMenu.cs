using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour, IOpenMenu
{
    [SerializeField]
    private Button[] starsLevel;

    void Start()
    {
        for(int i = 0; i < starsLevel.Length; i++)
        {
            if (SaveParameters.levelStars[i] > 0 && SaveParameters.levelStars[i] <= 3)
            {
                starsLevel[i].interactable = true;
                starsLevel[i].GetComponentsInChildren<Text>()[1].text = $"Получено {SaveParameters.levelStars[i]} звезда(ы)";
            }
            else
            {
                starsLevel[i].interactable = false;
                if (i == 0)
                {
                    starsLevel[i].interactable = true;
                }
                starsLevel[i].GetComponentsInChildren<Text>()[1].text = "Не пройдено";
            }
        }
    }

    public void OnLevel(int level)
    {
        SaveParameters.levelActive = level;
        SceneManager.LoadScene(level);
    }

    public void OpenMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
