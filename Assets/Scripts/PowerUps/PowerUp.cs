using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public event EventHandler OnPowerUp;
    private bool isCollected = false;
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (isCollected) return;
        if (collision2D.gameObject.GetComponent<PaddleMovement>())
        {
            isCollected = true;
            OnPowerUp?.Invoke(this, EventArgs.Empty);
            Debug.Log("Chạm paddle");
            Destroy(gameObject);
        }
        if (collision2D.gameObject.GetComponent<WallBottom>())
        {
            Destroy(gameObject);
        }
    }
}
