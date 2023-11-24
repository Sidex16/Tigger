using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    private Transform progressBar;
    [SerializeField]
    private Transform finalPosition;

    private void Update()
    {
        progressBar.position = Vector3.MoveTowards(progressBar.position, finalPosition.position, Time.deltaTime * 2);
        
        if(progressBar.position == finalPosition.position)
        {
            SceneManager.LoadScene(1);
        }
    }
}
