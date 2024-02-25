﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Button _levelButton;
    [SerializeField] private TextMeshProUGUI _completedText;

    public Button LevelButton => _levelButton;
    public TextMeshProUGUI CompletedText => _completedText;
}