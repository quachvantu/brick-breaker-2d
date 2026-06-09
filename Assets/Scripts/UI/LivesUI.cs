using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private Image[] heartImages;
    private void Start()
    {
        GameManager.Instance.OnLivesChanged += GameManager_OnLivesChanged;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnLivesChanged -= GameManager_OnLivesChanged;
    }
    private void GameManager_OnLivesChanged(int lives)
    {
        UpdateLivesUI(lives);
    }
    private void UpdateLivesUI(int lives)
    {
        for (int i = heartImages.Length - 1; i >= 0; i--)
        {
            heartImages[i].enabled = i < lives;
        }
    }

}
