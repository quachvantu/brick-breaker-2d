using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private PowerUpType powerUpType = PowerUpType.None;
    public static event EventHandler<PowerUpType> OnPowerUp;
    private bool isCollected = false;
    [SerializeField] private Animator powerUpAnimator;
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (isCollected) return;
        if (collision2D.gameObject.GetComponent<Paddle>())
        {
            isCollected = true;
            OnPowerUp?.Invoke(this, powerUpType);
            Destroy(gameObject);
        }
        if (collision2D.gameObject.GetComponent<WallBottom>())
        {
            Destroy(gameObject);
        }
    }
    public void SetPowerUpType(PowerUpType powerUpType)
    {
        this.powerUpType = powerUpType;
        if (powerUpType == PowerUpType.Score100)
        {
            powerUpAnimator.enabled = true;
        }
    }
}
