using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerSettings : MonoBehaviour
{
    [SerializeField]
    private SoundEffectsSO audioClips;


    private void OnEnable()
    {
        SettingsUI.OnButtonClick += SettingsUI_OnButtonClick; ;
    }

    private void SettingsUI_OnButtonClick(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.onButtonClick, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }


    private void OnDisable()
    {
        SettingsUI.OnButtonClick -= SettingsUI_OnButtonClick;
    }
}
