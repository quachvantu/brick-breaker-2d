using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }
    public event EventHandler OnIsFireBall;
    public event EventHandler OffIsFireBall;
    public event EventHandler OnIsBombBall;
    public event EventHandler OffIsBombBall;
    private event EventHandler OnIsShootPaddle;
    private bool isFireBall = false;
    private bool isBombBall = false;
    private bool isShootPaddle = false;
    private class Timer
    {
        public float elapsed;
        public float duration;
        public Action OnExpire;
        public string tag;

    }
    private List<Timer> timers = new List<Timer>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PowerUp.OnPowerUp -= PowerUp_OnPowerUp;
            PowerUp.OnPowerUp += PowerUp_OnPowerUp;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            PowerUp.OnPowerUp -= PowerUp_OnPowerUp;
            Instance = null;
        }
    }
    private void Update()
    {
        for (int i = timers.Count - 1; i >= 0; i--)
        {
            timers[i].elapsed += Time.deltaTime;
            if (timers[i].elapsed >= timers[i].duration)
            {
                timers[i].OnExpire();
                timers.RemoveAt(i);
            }
        }
    }
    private void PowerUp_OnPowerUp(object sender, PowerUpType e)
    {
        switch (e)
        {
            case PowerUpType.WidthIncrease:
                if (isShootPaddle)
                {
                    isShootPaddle = false;
                }
                Paddle.Instance.PaddleWidthIncrease();
                StartTimer(10f,
                () =>
                {
                    Paddle.Instance.ResetPaddleWidth();
                }, "paddle");
                break;
            case PowerUpType.WidthDecrease:
                if (isShootPaddle)
                {
                    isShootPaddle = false;
                }
                Paddle.Instance.PaddleWidthDecrease();
                StartTimer(10f,
                () =>
                {
                    Paddle.Instance.ResetPaddleWidth();
                }, "paddle");
                break;
            case PowerUpType.Score50:
                GameManager.Instance.AddScore(50);
                break;
            case PowerUpType.Score100:
                GameManager.Instance.AddScore(100);
                break;
            case PowerUpType.Score250:
                GameManager.Instance.AddScore(250);
                break;
            case PowerUpType.Score500:
                GameManager.Instance.AddScore(500);
                break;
            case PowerUpType.Slow:
                Ball.Instance.DecreaseSpeed(2f);
                StartTimer(10f, Ball.Instance.ResetSpeed, "ball_slow");
                break;
            case PowerUpType.Flash:
                Ball.Instance.IncreaseSpeed(2f);
                StartTimer(10f, Ball.Instance.ResetSpeed, "ball_flash");
                break;
            case PowerUpType.MultiBall:
                Ball.Instance.CreateBall();
                Ball.Instance.CreateBall();
                break;
            case PowerUpType.FireBall:
                if (isBombBall)
                {
                    isBombBall = false;
                    OffIsBombBall?.Invoke(this, EventArgs.Empty);
                }
                isFireBall = true;
                OnIsFireBall?.Invoke(this, EventArgs.Empty);
                StartTimer(5f, () =>
                {
                    isFireBall = false;
                    OffIsFireBall?.Invoke(this, EventArgs.Empty);
                }, "fire_ball");
                break;
            case PowerUpType.BombBall:
                if (isFireBall)
                {
                    isFireBall = false;
                    OffIsFireBall?.Invoke(this, EventArgs.Empty);
                }
                isBombBall = true;
                OnIsBombBall?.Invoke(this, EventArgs.Empty);
                StartTimer(5f, () =>
                {
                    isBombBall = false;
                    OffIsBombBall?.Invoke(this, EventArgs.Empty);
                }, "bomb_ball");
                break;
            case PowerUpType.PaddleShoot:
                isShootPaddle = true;
                OnIsShootPaddle?.Invoke(this, EventArgs.Empty);
                Paddle.Instance.PaddleShoot();
                StartTimer(10f, () => isShootPaddle = false, "shoot_paddle");
                break;
        }
    }
    private void StartTimer(float duration, Action action, string tag)
    {
        for (int i = timers.Count - 1; i >= 0; i--)
        {
            if (timers[i].tag == tag)
            {
                timers.RemoveAt(i);
            }
        }
        timers.Add(new Timer
        {
            elapsed = 0f,
            duration = duration,
            OnExpire = action,
            tag = tag
        });
    }
    public bool GetIsFireBall()
    {
        return isFireBall;
    }
    public bool GetIsBombBall()
    {
        return isBombBall;
    }
    public bool GetIsShootPaddle()
    {
        return isShootPaddle;
    }
}
