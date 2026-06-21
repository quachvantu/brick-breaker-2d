using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireTrailRenderer : MonoBehaviour
{
    private void Start()
    {
        PowerUpManager.Instance.OnIsFireBall += PowerUpManager_OnIsFireBall;
        PowerUpManager.Instance.OffIsFireBall += PowerUpManager_OffIsFireBall;
        gameObject.SetActive(PowerUpManager.Instance.GetIsFireBall());
    }
    private void OnDestroy()
    {
        if (PowerUpManager.Instance == null) return;
        PowerUpManager.Instance.OnIsFireBall -= PowerUpManager_OnIsFireBall;
        PowerUpManager.Instance.OffIsFireBall -= PowerUpManager_OffIsFireBall;
    }
    private void PowerUpManager_OffIsFireBall(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void PowerUpManager_OnIsFireBall(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
