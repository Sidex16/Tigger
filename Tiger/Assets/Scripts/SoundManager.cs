using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private SoundEffectsSO audioClips;

    private bool canPlaySound = true;

    private void Start()
    {
        UI.Instance.OnButtonClick += UI_OnButtonClick;
        UI.Instance.OnAutoClick += UI_OnAutoClick;
        UI.Instance.OnX2Click += UI_OnX2Click;
        UI.Instance.OnRecordBeat += UI_OnRecordBeat;
        Coin.OnGetCoin += Coin_OnGetCoin; 
        Coin.OnLoseCoin += Coin_OnLoseCoin; 
        FakeCoin.OnGetFakeCoin += FakeCoin_OnGetFakeCoin;
        Obstacle.OnJumpFailed += Obstacle_OnJumpFailed;
        TouchManager.Instance.OnTap += TouchManager_OnTap;
        GameManager.Instance.OnAbilityFinish += GameManager_OnAbilityFinish;
    }

    private void TouchManager_OnTap(object sender, EventArgs e)
    {
        PlaySound(audioClips.jump, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void Obstacle_OnJumpFailed(object sender, EventArgs e)
    {
        if (canPlaySound)
        {
            PlaySound(audioClips.bump, Vector3.zero, MusicManager.Instance.GetSoundVolume());
            StartCoroutine(ResetSoundCooldown());
        }
    }

    private void GameManager_OnAbilityFinish(object sender, EventArgs e)
    {
        PlaySound(audioClips.abilityFinish, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void UI_OnRecordBeat(object sender, EventArgs e)
    {
        PlaySound(audioClips.newRecord, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void FakeCoin_OnGetFakeCoin(object sender, EventArgs e)
    {
        PlaySound(audioClips.gameOver, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void Coin_OnLoseCoin(object sender, EventArgs e)
    {
        if (canPlaySound)
        {
            PlaySound(audioClips.loseCoin, Vector3.zero, MusicManager.Instance.GetSoundVolume() / 2);
            StartCoroutine(ResetSoundCooldown());
        }
    }


    private void Coin_OnGetCoin(object sender, EventArgs e)
    {
        
        if (canPlaySound)
        {
            PlaySound(audioClips.getCoin, Vector3.zero, MusicManager.Instance.GetSoundVolume());
            StartCoroutine(ResetSoundCooldown());
        }
    }

    private void UI_OnX2Click(object sender, EventArgs e)
    {
        PlaySound(audioClips.abilityStart, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void UI_OnAutoClick(object sender, EventArgs e)
    {
        PlaySound(audioClips.abilityStart,Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void UI_OnButtonClick(object sender, EventArgs e)
    {
        PlaySound(audioClips.onButtonClick,Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 0.5f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    private IEnumerator ResetSoundCooldown()
    {
        canPlaySound = false;
        yield return new WaitForSeconds(0.1f);
        canPlaySound = true;
    }

    private void OnDisable()
    {
        Coin.OnGetCoin -= Coin_OnGetCoin;
        Coin.OnLoseCoin -= Coin_OnLoseCoin;
        FakeCoin.OnGetFakeCoin -= FakeCoin_OnGetFakeCoin;
        Obstacle.OnJumpFailed -= Obstacle_OnJumpFailed;
    }
}
