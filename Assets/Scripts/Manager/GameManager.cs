using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public State state;
    private static int lives = 3;
    private int totalScore = 0;
    public enum State
    {
        Menu,
        Playing,
        GameOver
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
        Ball.Instance.OnBallLost += Ball_OnBallLost;
    }
    private void Update()
    {
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
            case State.GameOver:
                break;
        }
    }
    private void Ball_OnBallLost(object sender, EventArgs e)
    {
        lives--;
    }
    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log("Score: " + totalScore);
    }
    public void SetState(State state)
    {
        this.state = state;
        Handle();
    }
}
