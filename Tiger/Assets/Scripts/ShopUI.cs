using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }

    public event EventHandler OnButtonClick;
    public event EventHandler OnPurchaceSuccessful;
    public event EventHandler OnPurchaceFailed;

    [SerializeField]
    private Button buyX2;
    [SerializeField]
    private Button buyAuto;
    [SerializeField]
    private Button mainMenu;
    [SerializeField]
    TextMeshProUGUI myMoney;

    SaveManager.PlayerData playerData;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadData();
        UpdateBalance();

        buyX2.onClick.AddListener(() =>
        {
            if (playerData.playerMoney > 400)
            {
                playerData.playerMoney -= 400;
                playerData.x2Number++;
                SaveManager.SavePlayerData(playerData);
                UpdateBalance();
                OnPurchaceSuccessful?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnPurchaceFailed?.Invoke(this, EventArgs.Empty);
            }

        });

        buyAuto.onClick.AddListener(() =>
        {
            if (playerData.playerMoney > 600)
            {
                playerData.playerMoney -= 600;
                playerData.autoNumber++;
                SaveManager.SavePlayerData(playerData);
                UpdateBalance();
                OnPurchaceSuccessful?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnPurchaceFailed?.Invoke(this, EventArgs.Empty);
            }

        });

        mainMenu.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(1);
        });
    }

    private void LoadData()
    {
        playerData = SaveManager.LoadPlayerData();
    }

    private void UpdateBalance()
    {
        myMoney.text = playerData.playerMoney.ToString();
    }

}
