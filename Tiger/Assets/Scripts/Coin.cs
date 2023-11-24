using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event EventHandler OnGetCoin;
    public static event EventHandler OnLoseCoin;

    [SerializeField]
    private Transform player;  

    private Vector3 playerPos;

    private void Update()
    {
        if (!GameManager.Instance.GetIsGameOver())
        {
            if (TouchManager.Instance.GetIsTouched() || GameManager.Instance.GetIsAuto())
            {

                if (transform.position.y < -0.5f && transform.position.y > -3.3f)
                {
                    float speed = 9f;
                    playerPos = player.position + new Vector3(0, 1.2f, 0);
                    transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * speed);
                }

            }

            if (transform.position.y > -4f && transform.position.y < -3.1f && transform.position.x > -0.5f && transform.position.x < 0.5f)
            {
                Destroy(gameObject);
                OnGetCoin?.Invoke(this, EventArgs.Empty);
            }
            if (transform.position.y < -5f)
            {
                Destroy(gameObject);
                OnLoseCoin?.Invoke(this, EventArgs.Empty);
            }
        }

    }


}
