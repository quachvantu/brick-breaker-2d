using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;


public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button buttonTryAgain;
    [SerializeField] private Button buttonMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        buttonTryAgain.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.Playing);
            GameManager.Instance.SetState(GameManager.State.Playing);
        });
        buttonMenu.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.Menu);
            GameManager.Instance.SetState(GameManager.State.Menu);
        });
        HideGameOverUI();
    }
    public void ShowGameOverUI()
    {
        scoreText.text = "SCORE " + GameManager.Instance.GetScore();
        int minutes = Mathf.FloorToInt(GameManager.Instance.GetTimer() / 60);
        int seconds = Mathf.FloorToInt(GameManager.Instance.GetTimer() % 60);
        timerText.text = string.Format("TIMER {0:00}:{1:00}", minutes, seconds);
        gameObject.SetActive(true);
    }
    public void HideGameOverUI()
    {
        gameObject.SetActive(false);
    }
}
