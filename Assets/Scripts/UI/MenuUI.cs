using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    public Slider speedBallSlider;
    public Slider speedPaddleSlider;
    private void Awake()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.Playing);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    private void Start()
    {
        speedBallSlider.value = GameManager.Instance.ballSpeed;
        speedBallSlider.onValueChanged.AddListener(OnSpeedChanged);
        speedPaddleSlider.value = GameManager.Instance.paddleSpeed;
        speedPaddleSlider.onValueChanged.AddListener(OnSpeedPaddleChanged);
        Debug.Log(GameManager.Instance.paddleSpeed);
    }

    private void OnSpeedPaddleChanged(float value1)
    {
        GameManager.Instance.paddleSpeed = value1;
    }

    private void OnSpeedChanged(float value)
    {
        GameManager.Instance.ballSpeed = value;
    }
}
