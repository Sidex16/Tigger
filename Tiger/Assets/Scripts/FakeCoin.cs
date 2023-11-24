using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCoin : MonoBehaviour
{
    public static event EventHandler OnGetFakeCoin;

    [SerializeField]
    private Transform player;

    private Vector3 playerPos;

    private void Update()
    {
        if (!GameManager.Instance.GetIsGameOver())
        {
            if (TouchManager.Instance.GetIsTouched())
            {
                if (transform.position.y < -0.5f && transform.position.y > -2.9f)
                {
                    float speed = 4f;
                    playerPos = player.position + new Vector3(0, 1.2f, 0);
                    transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * speed);
                }

            }
            if (transform.position.y > -4f && transform.position.y < -3.1f && transform.position.x > -0.35f && transform.position.x < 0.35f)
            {
                Destroy(gameObject);
                GameManager.Instance.SetIsGameOver(true);
                OnGetFakeCoin?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
