using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundEffectsSO : ScriptableObject
{
    public AudioClip getCoin;
    public AudioClip loseCoin;
    public AudioClip gameOver;
    public AudioClip newRecord;
    public AudioClip onButtonClick;
    public AudioClip abilityStart;
    public AudioClip abilityFinish;
    public AudioClip successfulPurchase;
    public AudioClip failedPurchase;
    public AudioClip jump;
    public AudioClip bump;
}
