using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private void Start()
    {
        GameManager.Instance.OnScoreChanged += GameManager_OnScoreChanged;
    }

    private void GameManager_OnScoreChanged(int obj)
    {
        scoreText.text = "SCORE " + obj;
    }
}