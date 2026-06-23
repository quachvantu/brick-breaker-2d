using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class FireTrailRenderer : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private TrailRenderer trailRenderer_1;
    [SerializeField] private VisualEffect trailParticle;
    [SerializeField] private Light pointLight;
    private void Start()
    {
        PowerUpManager.Instance.OnIsFireBall += PowerUpManager_OnIsFireBall;
        PowerUpManager.Instance.OffIsFireBall += PowerUpManager_OffIsFireBall;
        SetVisualActive(PowerUpManager.Instance.GetIsFireBall());
    }
    private void OnDestroy()
    {
        if (PowerUpManager.Instance == null) return;
        PowerUpManager.Instance.OnIsFireBall -= PowerUpManager_OnIsFireBall;
        PowerUpManager.Instance.OffIsFireBall -= PowerUpManager_OffIsFireBall;
    }
    private void PowerUpManager_OffIsFireBall(object sender, EventArgs e)
    {
        SetVisualActive(false);
    }
    private void PowerUpManager_OnIsFireBall(object sender, EventArgs e)
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
