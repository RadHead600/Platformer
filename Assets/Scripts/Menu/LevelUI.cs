using UnityEngine;
using UnityEngine.UI;

public class LevelUI
{
    [SerializeField] private Button _levelButton;
    [SerializeField] private Text _completedText;

    public Button LevelButton => _levelButton;
    public Text CompletedText => _completedText;
}