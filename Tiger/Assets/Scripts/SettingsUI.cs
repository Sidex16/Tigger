using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static event EventHandler OnButtonClick;

    [SerializeField]
    private Button musicOnOff;
    [SerializeField]
    private Button soundOnOff;
    [SerializeField]
    private Button mainMenu;

    [SerializeField]
    private SettingsButtonsSO settingsButtons;

    private void Start()
    {
        musicOnOff.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);

            Image musicImage = musicOnOff.GetComponent<Image>();

            if (MusicManager.Instance.GetMusicVolume() == 0)
            {
                MusicManager.Instance.SetMusicVolume(0.15f);
                musicImage.sprite = settingsButtons.activeButtons[0];
            }
            else
            {
                MusicManager.Instance.SetMusicVolume(0);
                musicImage.sprite = settingsButtons.inactiveButtons[0];
            }

        });

        soundOnOff.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);

            Image soundImage = soundOnOff.GetComponent<Image>();

            if (MusicManager.Instance.GetSoundVolume() == 0)
            {
                MusicManager.Instance.SetSoundVolume(0.6f);
                soundImage.sprite = settingsButtons.activeButtons[1];
            }
            else
            {
                MusicManager.Instance.SetSoundVolume(0);
                soundImage.sprite = settingsButtons.inactiveButtons[1];
            }
        });

        mainMenu.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(1);
        });
    }

}
