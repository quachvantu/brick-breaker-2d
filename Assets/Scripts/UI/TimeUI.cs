using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private void Start()
    {
        GameManager.Instance.OnTimeChanged += GameManager_OnTimeChanged;
    }

    private void GameManager_OnTimeChanged(float obj)
    {
        int minutes = Mathf.FloorToInt(obj / 60);
        int seconds = Mathf.FloorToInt(obj % 60);
        timeText.text = string.Format("TIMER {00}:{00}", minutes, seconds);
    }
}
