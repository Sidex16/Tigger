using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        TouchManager.Instance.OnTap += TouchManager_OnTap;
    }

    private void TouchManager_OnTap(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.GetIsGameOver())
        {
            animator.SetTrigger("Jump");
        }
    }
}
