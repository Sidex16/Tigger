using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerMainMenu : MonoBehaviour
{
    [SerializeField]
    private SoundEffectsSO audioClips;


    private void OnEnable()
    {
        MainMenuUI.OnButtonClick += MainMenuUI_OnButtonClick;
    }

    private void MainMenuUI_OnButtonClick(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.onButtonClick, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void OnDisable()
    {
        MainMenuUI.OnButtonClick -= MainMenuUI_OnButtonClick;
    }
}
