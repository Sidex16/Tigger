using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerShop : MonoBehaviour
{
    [SerializeField]
    private SoundEffectsSO audioClips;


    private void OnEnable()
    {
        ShopUI.Instance.OnButtonClick += ShopUI_OnBUttonClick;
        ShopUI.Instance.OnPurchaceFailed += ShopUI_OnPurchaceFailed;
        ShopUI.Instance.OnPurchaceSuccessful += ShopUI_OnPurchaceSuccessful;
    }

    private void ShopUI_OnPurchaceSuccessful(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.successfulPurchase, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void ShopUI_OnPurchaceFailed(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.failedPurchase, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void ShopUI_OnBUttonClick(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.onButtonClick, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }


    private void OnDisable()
    {
        SettingsUI.OnButtonClick -= ShopUI_OnBUttonClick;
    }
}
