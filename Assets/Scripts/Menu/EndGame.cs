using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject _menuEndGame;
    [SerializeField] private GameObject _lossMenu;
    [SerializeField] private List<float> _completeStarPercent;
    [SerializeField] private Image[] _stars;
    [SerializeField] private Text _textPoints;
    [SerializeField] private Text _textMoney;

    private delegate Material AddStarDelegate(int starNum);

    private int _pointsCount;
    private AddStarDelegate _addStars;

    void Start()
    {
        Enemy[] enemys = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemys)
        {
            _pointsCount += enemy.Points;
        }

        _menuEndGame.SetActive(false);
        _lossMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Unit unit))
        {
            CountingLevelStars();
        }
    }

    public void LoseCanvas()
    {
        Time.timeScale = 0;
        _lossMenu.SetActive(true);
    }

    private void CountingLevelStars()
    {
        Time.timeScale = 0;

        _menuEndGame.SetActive(true);

        int points = SaveParameters.levelPoints[SaveParameters.levelActive];

        _addStars += AddStar;

        for (int i = 0; i < _completeStarPercent.Count; i++)
        {
            if (points >= _pointsCount * _completeStarPercent[i])
            {
                SaveParameters.levelStars[SaveParameters.levelActive] = i + 1;
            }
        }

        SaveParameters.money = points / 100;

        _textPoints.text = points.ToString();
        _textMoney.text = SaveParameters.money.ToString();

        SaveParameters.levelComplete[SaveParameters.levelActive] = true;

        _addStars(SaveParameters.levelStars[SaveParameters.levelActive]);
    }

    private Material AddStar(int starNum)
    {
        return _stars[starNum].material = null;
    }

    private void OnDestroy()
    {
        _addStars -= AddStar;
    }
}
