using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<int> OnLivesChanged;
    public event Action<int> OnScoreChanged;
    public event Action<float> OnTimeChanged;
    public State state;
    private static int lives = 3;
    private static int totalScore = 0;
    public float ballSpeed = 7f;
    public float paddleSpeed = 10f;
    private bool isTimerRunning = false;
    private float timer = 0f;
    public enum State
    {
        Menu,
        Playing,
        GameOver,
        YouWin
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        // state = State.Menu;
        // Handle();
        SetState(State.Menu);
    }
    private void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            OnTimeChanged?.Invoke(timer);
        }
    }
    public void Handle()
    {
        switch (state)
        {
            case State.Menu:
                break;
            case State.Playing:
                ResetScore();
                ResetTimer();
                ResetLives();
                break;
            case State.YouWin:
                break;
            case State.GameOver:
                StopTimer();
                GameOverUI.Instance.ShowGameOverUI();
                break;
        }
    }
    private void Ball_OnBallLost(object sender, EventArgs e)
    {
        lives--;
        OnLivesChanged?.Invoke(lives);
        if (lives <= 0)
        {
            SetState(State.GameOver);
        }
        else
        {
            Ball.Instance.SetIsLaunched(false);
        }
    }
    public void AddScore(int score)
    {
        totalScore += score;
        OnScoreChanged?.Invoke(totalScore);
    }
    public void SetState(State state)
    {
        this.state = state;
        Handle();
    }

    public void RegisterBall(Ball ball)
    {
        ball.OnBallLost += Ball_OnBallLost;
    }
    public void UnregisterBall(Ball ball)
    {
        ball.OnBallLost -= Ball_OnBallLost;
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }
    public void ResetTimer()
    {
        timer = 0f;
    }
    public void StopTimer()
    {
        isTimerRunning = false;
    }
    public int GetScore()
    {
        return totalScore;
    }
    public float GetTimer()
    {
        return timer;
    }
    public void ResetScore()
    {
        totalScore = 0;
    }
    public void ResetLives()
    {
        lives = 3;
    }
}
