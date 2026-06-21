using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrailRenderer : MonoBehaviour
{
    private void Start()
    {
        PowerUpManager.Instance.OnIsBombBall += PowerUpManager_OnIsBombBall;
        PowerUpManager.Instance.OffIsBombBall += PowerUpManager_OffIsBombBall;
        gameObject.SetActive(PowerUpManager.Instance.GetIsBombBall());
    }
    private void OnDestroy()
    {
        if (PowerUpManager.Instance == null) return;
        PowerUpManager.Instance.OnIsBombBall -= PowerUpManager_OnIsBombBall;
        PowerUpManager.Instance.OffIsBombBall -= PowerUpManager_OffIsBombBall;
    }
    private void PowerUpManager_OffIsBombBall(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }
    private void PowerUpManager_OnIsBombBall(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
