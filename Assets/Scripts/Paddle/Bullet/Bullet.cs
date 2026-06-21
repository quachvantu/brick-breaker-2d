using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 8f;
    private void Update()
    {
        transform.position += Vector3.up * bulletSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.GetComponent<RectangleBrick>() || collision2D.gameObject.GetComponent<SquareBrick>())
        {
            Destroy(gameObject);
        }
    }
}
