using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private float lowerBound = -10f;

    private void Update()
    {
        speed = GameManager.Instance.GetIsAuto() ? 10f : 5f;

        if (!GameManager.Instance.GetIsGameOver())
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }
    }

}

