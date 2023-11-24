using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static event EventHandler OnJumpFailed;

    private bool isPlayedOnce = false;


    private void Update()
    {
        if (transform.position.y < -3 && transform.position.y > -5 && !GameManager.Instance.GetIsOnJump())
        {
            GameManager.Instance.SetIsGameOver(true);
            if (!isPlayedOnce)
            {
                OnJumpFailed?.Invoke(this, EventArgs.Empty);
                isPlayedOnce = true;
            }
        }

    }
}
