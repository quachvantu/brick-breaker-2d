using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BombTrailRenderer : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private TrailRenderer trailRenderer_1;
    [SerializeField] private VisualEffect trailParticle;
    [SerializeField] private Light pointLight;
    private void Start()
    {
        PowerUpManager.Instance.OnIsBombBall += PowerUpManager_OnIsBombBall;
        PowerUpManager.Instance.OffIsBombBall += PowerUpManager_OffIsBombBall;
        SetVisualActive(PowerUpManager.Instance.GetIsBombBall());
    }
    private void OnDestroy()
    {
        if (PowerUpManager.Instance == null) return;
        PowerUpManager.Instance.OnIsBombBall -= PowerUpManager_OnIsBombBall;
        PowerUpManager.Instance.OffIsBombBall -= PowerUpManager_OffIsBombBall;
    }
    private void PowerUpManager_OffIsBombBall(object sender, EventArgs e)
    {
        SetVisualActive(false);
    }
    private void PowerUpManager_OnIsBombBall(object sender, EventArgs e)
    {
        SetVisualActive(true);
    }
    private void SetVisualActive(bool active)
    {
        trailRenderer.enabled = active;
        trailRenderer_1.enabled = active;
        trailParticle.enabled = active;
        pointLight.enabled = active;
    }
}
