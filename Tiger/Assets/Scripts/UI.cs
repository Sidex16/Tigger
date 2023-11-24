using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }

    public event EventHandler OnAutoClick;
    public event EventHandler OnX2Click;
    public event EventHandler OnButtonClick;
    public event EventHandler OnRecordBeat;

    [SerializeField]
    private Button autoAbility;
    [SerializeField]
    private Button x2Ability;
    [SerializeField]
    private Button pause;
    [SerializeField]
    private Button restart;
    [SerializeField]
    private Button[] mainMenuList;
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI goScore;
    [SerializeField]
    private TextMeshProUGUI autoText;
    [SerializeField]
    private TextMeshProUGUI x2Text;
    [SerializeField]
    private Button continueGameplay;


    [SerializeField]
    private GameObject pauseGameObject;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject cup;

    SaveManager.PlayerData playerData;

    private int recordIndex;
    private int scoreIndex;
    private int x2Number;
    private int autoNumber;

    private void Awake()
    {
        scoreIndex = 0;
        if (Instance == null)
            Instance = this;

        pauseGameObject.SetActive(false);
        gameOver.SetActive(false);
        cup.SetActive(false);

        LoadAllData();

        AddListeners();
    }

    private void OnEnable()
    {
        Coin.OnGetCoin += Coin_OnGetCoin;
        Coin.OnLoseCoin += Coin_OnLoseCoin;
        FakeCoin.OnGetFakeCoin += FakeCoin_OnGetFakeCoin;
        Obstacle.OnJumpFailed += Obstacle_OnJumpFailed;
    }

    private void Obstacle_OnJumpFailed(object sender, EventArgs e)
    {
        TriggerGameOver();
    }

    private void Coin_OnLoseCoin(object sender, EventArgs e)
    {
        RemoveFromScore();
    }

    private void FakeCoin_OnGetFakeCoin(object sender, System.EventArgs e)
    {
        TriggerGameOver();
    }

    private void Coin_OnGetCoin(object sender, System.EventArgs e)
    {
        AddToScore();
    }

    private void AddToScore()
    {
        scoreIndex += GameManager.Instance.GetIsX2() ? 2 : 1;

        score.text = scoreIndex.ToString();
    }

    private void RemoveFromScore()
    {
        scoreIndex--;

        score.text = scoreIndex.ToString();
    }

    private void TriggerGameOver()
    {
        SaveAllData();

        gameOver.SetActive(true);
        goScore.text = scoreIndex.ToString();
    }

    private void LoadAllData()
    {
        playerData = SaveManager.LoadPlayerData();

        recordIndex = playerData.playerRecord;
        autoNumber = playerData.autoNumber;
        x2Number = playerData.x2Number;

        x2Text.text = x2Number.ToString();
        autoText.text = autoNumber.ToString();
    }

    private void SaveAllData()
    {
        playerData.autoNumber = autoNumber;
        playerData.x2Number = x2Number;
        playerData.playerMoney += scoreIndex;
        if (scoreIndex > recordIndex)
        {
            recordIndex = scoreIndex;
            playerData.playerRecord = recordIndex;
            OnRecordBeat?.Invoke(this, EventArgs.Empty);
            cup.SetActive(true);
        }
        SaveManager.SavePlayerData(playerData);
    }

    private void AddListeners()
    {
        autoAbility.onClick.AddListener(() =>
        {
            if (!GameManager.Instance.GetIsGameOver() && autoNumber > 0 && !GameManager.Instance.GetIsAuto())
            {
                OnAutoClick?.Invoke(this, EventArgs.Empty);
                autoNumber--;
                autoText.text = autoNumber.ToString();
            }
        });

        x2Ability.onClick.AddListener(() =>
        {
            if (!GameManager.Instance.GetIsGameOver() && x2Number > 0 && !GameManager.Instance.GetIsX2())
            {
                OnX2Click?.Invoke(this, EventArgs.Empty);
                x2Number--;
                x2Text.text = x2Number.ToString();
            }
        });

        pause.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);

            if (!GameManager.Instance.GetIsGameOver())
            {
                GameManager.Instance.SetIsGameOver(true);
                pauseGameObject.SetActive(true);
            }

        });

        continueGameplay.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            GameManager.Instance.SetIsGameOver(false);
            pauseGameObject.SetActive(false);
        });

        restart.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            GameManager.Instance.SetIsGameOver(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        foreach (Button mainMenu in mainMenuList)
        {
            mainMenu.onClick.AddListener(() =>
            {
                OnButtonClick?.Invoke(this, EventArgs.Empty);
                SceneManager.LoadScene(1);
            });
        }
    }

    public int GetAutoNumber()
    {
        return autoNumber;
    }

    public void AddToAutoNumber(int number)
    {
        autoNumber += number;
    }

    public int GetX2Number()
    {
        return x2Number;
    }

    public void AddToX2Number(int number)
    {
        x2Number += number;
    }

    private void OnDisable()
    {
        Coin.OnGetCoin -= Coin_OnGetCoin;
        Coin.OnLoseCoin -= Coin_OnLoseCoin;
        FakeCoin.OnGetFakeCoin -= FakeCoin_OnGetFakeCoin;
        Obstacle.OnJumpFailed -= Obstacle_OnJumpFailed;
    }
}
