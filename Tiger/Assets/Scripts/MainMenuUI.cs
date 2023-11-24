using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static event EventHandler OnButtonClick;

    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button shopButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private TextMeshProUGUI recordText;

    private void Awake()
    {
        //SaveManager.ClearAllData();

        recordText.text = SaveManager.LoadPlayerData().playerRecord.ToString();

        playButton.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(2);
        });

        shopButton.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(3);
        });

        settingsButton.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(4);
        });
        
        exitButton.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            Application.Quit();
        });
    }
}
