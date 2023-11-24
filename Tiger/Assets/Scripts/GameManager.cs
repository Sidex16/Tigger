using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnAbilityFinish;

    [SerializeField]
    private GameObject tutorial;
    [SerializeField]
    private Button hideButton;

    SaveManager.PlayerData playerData;

    private float abilityTimer;
    private float abilityTimerMax = 5f;
    private float jumpTimer;
    private float jumpTimerMax = 1f;

    private bool isX2;
    private bool IsAuto;
    private bool isOnJump;
    private bool isGameOver;




    private void Awake()
    {
        

        if (Instance == null)
            Instance = this;

        isGameOver = false;
    }

    private void Start()
    {
        UI.Instance.OnAutoClick += UI_OnAutoClick;
        UI.Instance.OnX2Click += UI_OnX2Click;
        TouchManager.Instance.OnTap += TouchManager_OnTap;

        playerData = SaveManager.LoadPlayerData();

        if (playerData.isFitrsPlay)
        {
            tutorial.SetActive(true);
            isGameOver = true;
            playerData.isFitrsPlay = false;
            SaveManager.SavePlayerData(playerData);
            hideButton.onClick.AddListener(() =>
            {
                tutorial.SetActive(false);
                isGameOver = false;
            });
        }
        else
        {
            tutorial.SetActive(false);
        }
    }

    private void TouchManager_OnTap(object sender, EventArgs e)
    {
        jumpTimer = jumpTimerMax; ;
        isOnJump = true;
    }

    private void UI_OnAutoClick(object sender, System.EventArgs e)
    {
        abilityTimer = abilityTimerMax;
        IsAuto = true;
        isOnJump = true;
    }

    private void UI_OnX2Click(object sender, System.EventArgs e)
    {
        abilityTimer = abilityTimerMax;
        isX2 = true;
    }

    private void Update()
    {
        abilityTimer -= Time.deltaTime;
        jumpTimer -= Time.deltaTime;


        if (jumpTimer < 0 && !IsAuto)
        {
            isOnJump = false;
        }
        if (abilityTimer < 0)
        {
            if (isX2 || IsAuto)
                OnAbilityFinish?.Invoke(this, EventArgs.Empty);

            isX2 = false;
            IsAuto = false;
            isOnJump |= false;
        }

    }

    public bool GetIsX2() { return isX2; }
    public bool GetIsAuto() { return IsAuto; }
    public bool GetIsOnJump() { return isOnJump; }
    public bool GetIsGameOver() { return isGameOver; }

    public void SetIsGameOver(bool isGameOver)
    {
        this.isGameOver = isGameOver;
    }

}
