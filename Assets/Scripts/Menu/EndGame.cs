using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private GameObject menuEndGame;

    [SerializeField]
    private GameObject lossMenu;

    [SerializeField]
    private Image[] stars;

    [SerializeField]
    private Text textPoints;

    [SerializeField]
    private Text textMoney;

    private delegate Material AddStar();

    private int pointsCount;
    AddStar addStars;

    void Start()
    {
        Enemy[] enemys = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemys)
        {
            pointsCount += enemy.points;
        }

        menuEndGame.SetActive(false);
        lossMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Units unit = collision.GetComponent<Character>();
        if(unit != null)
        {
            CountingLevelStars();
        }
    }

    public void LossCanvas()
    {
        Time.timeScale = 0;
        lossMenu.SetActive(true);
    }

    private void CountingLevelStars()
    {
        Time.timeScale = 0;
        menuEndGame.SetActive(true);
        int points = SaveParameters.levelPoints[SaveParameters.levelActive];
        addStars = null;

        SaveParameters.levelStars[SaveParameters.levelActive] = 1;
        addStars += AddStarOne;

        if (points >= pointsCount * 0.6)
        {
            SaveParameters.levelStars[SaveParameters.levelActive] = 2;
            addStars += AddStarTwo;
        }
        if(points >= pointsCount)
        {
            SaveParameters.levelStars[SaveParameters.levelActive] = 3;
            addStars += AddStarThree;
        }

        SaveParameters.money = points / 100;

        textPoints.text = points.ToString();
        textMoney.text = SaveParameters.money.ToString();

        SaveParameters.levelComplete[SaveParameters.levelActive] = true;

        addStars();

        Time.timeScale = 0;
    }

    private Material AddStarOne()
    {
        return stars[0].material = null;
    }

    private Material AddStarTwo()
    {
        return stars[1].material = null;
    }

    private Material AddStarThree()
    {
        return stars[2].material = null;
    }
}
