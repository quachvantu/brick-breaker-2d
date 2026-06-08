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
    private int totalScore = 0;
    public float ballSpeed = 8f;
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
    private void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            OnTimeChanged?.Invoke(timer);
        }

        Handle();
    }
    private void Handle()
    {
        switch (state)
        {
            case State.Menu:
                break;
            case State.Playing:
                break;
            case State.YouWin:
                break;
            case State.GameOver:
                break;
        }
    }
    private void Ball_OnBallLost(object sender, EventArgs e)
    {
        lives--;
        OnLivesChanged?.Invoke(lives);
        if (lives <= 0)
        {
            state = State.GameOver;
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
        timer = 0f;
        isTimerRunning = true;
    }
    public void StopTimer()
    {
        isTimerRunning = false;
    }
}
