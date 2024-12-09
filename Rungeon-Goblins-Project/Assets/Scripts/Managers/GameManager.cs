using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    #region Headers

    [Header("UI components")]
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject scorePanel;
    [SerializeField] GameObject revivePanel;
    [SerializeField] Image reviveBackground;
    [SerializeField] TMP_Text reviveCostText;
    [SerializeField] TMP_Text reviveTimer;
    [SerializeField] TMP_Text[] coinHUD; // Todos os textos de Coins
    [SerializeField] TMP_Text[] distanceHUD; // Todos os textos de Distancia
    public Slider timeHud;

    [Header("Variables: ")]
    public PlayerData playerData;
    public SaveSystem saveSystem;
    public LoadSystem loadSystem;
    public AchievementSystem achievementSystem;
    [SerializeField] GameObject player;
    [SerializeField] PlayerMovement playerScript;
    [SerializeField] int coins;
    [SerializeField] int quickReviveTime;
    private int actualQuickReviveTime;
    [SerializeField] int distance;
    public int reviveCost = 50;
    private Timer timer;
    private bool hasRevived = false;
    private Vector3 deathPosition;
    [SerializeField] LocalizationManager localizationManager;

    #endregion

    private void Awake()
    {
        int language = PlayerPrefs.GetInt("language", 0);
        localizationManager.ChangeLocale(language);

        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            if (loadSystem.LoadPlayerData() == null)
            {
                playerData = new PlayerData();
                saveSystem.SavePlayerData(playerData);
            }
            else
            {
                playerData = loadSystem.LoadPlayerData();
            }

            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                UpdateData();
            }
        }

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerScript = player.GetComponent<PlayerMovement>();
            playerScript.gameManager = this;
            timer = GetComponent<Timer>();
            if (SceneManager.GetActiveScene().buildIndex != 2)
            {
                coinHUD[0].text = "$" + PlayerPrefs.GetInt("Money", 0);
            }

            coins = PlayerPrefs.GetInt("Money", 0);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        playerScript.SwitchCanMove(false);
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        playerScript.SwitchCanMove(true);
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("Money", coins);
        revivePanel.SetActive(false);
        StopAllCoroutines();
        UpdateData();
        AudioManager.Instance.PlayBGLoop(4);
        Pause();

        if (losePanel.activeSelf == false)
        {
            scorePanel.SetActive(true);
        }
        
        //losePanel.SetActive(true);
    }

    public IEnumerator ReviveQuickTime()
    {
        Pause();
        actualQuickReviveTime = quickReviveTime;
        reviveCostText.text = "REVIVE $" + reviveCost;
        revivePanel.SetActive(true);

        for (int i = actualQuickReviveTime; i > 0; i--)
        {
            reviveTimer.text = actualQuickReviveTime.ToString();
            actualQuickReviveTime--;
            reviveBackground.color = new Color(reviveBackground.color.r, reviveBackground.color.g, reviveBackground.color.b, 0.15f * i);
            yield return new WaitForSecondsRealtime(1);
        }

        if (hasRevived == false && scorePanel.activeSelf == false)
        {
            GameOver();
        }
    }

    public void AddCoin(int amount)
    {
        if (coins + amount >= 9999)
        {
            coins = 9999;
        }
        else 
        {
            coins += amount;
        }
        UpdateData();
    }

    public void AddDistance(int amount)
    {
        distance += amount;
        UpdateData();
    }

    void Update()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void UpdateData()
    {
        for (int i = 0; i < coinHUD.Length; i++)
        {
            coinHUD[i].text = "$" + coins.ToString();
        }

        for (int i = 0; i < distanceHUD.Length; i++)
        {
            distanceHUD[i].text = distance.ToString() + "m";
        }
        // int bestDistance = PlayerPrefs.GetInt("PersonalBest");

        // if (distance > bestDistance)
        // {
        //     PlayerPrefs.SetInt("PersonalBest", distance);
        // }
    }

    public void Revive()
    {
        revivePanel.SetActive(false);
        timer.GetUIComponents().SetActive(true);

        coins -= reviveCost;
        reviveCost += 50;

        UpdateData();

        Vector3 newPos = new Vector3((float)Math.Floor(deathPosition.x), 0, (float)Math.Floor(deathPosition.z - 20));

        string x = newPos.x.ToString(), z = newPos.z.ToString();

        if (x[x.Length - 1] != '0')
        {
            x = x.Substring(0, x.Length - 1) + '0';
        }

        if (z[z.Length - 1] != '5')
        {
            z = z.Substring(0, z.Length - 1) + '5';
        }

        newPos = new Vector3(float.Parse(x), newPos.y, float.Parse(z));

        playerScript.SetNextPosition(newPos);

        player.transform.position = newPos;

        playerScript.UpdatePosition();

        playerScript.AddCharge(playerScript.GetMaxCharges(), "Cheat");

        AddDistance(-2);

        playerScript.SetSpeed(playerScript.GetOriginalSpeed());

        playerScript.GetAnimator().Play("Idle");

        timer.AddTime(timer.GetMaxTime());
        UnPause();
    }

    public bool CanRevive()
    {
        if (coins >= reviveCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region getsSets
    public int GetDistance()
    {
        return distance;
    }

    public void SetRevived(bool state)
    {
        hasRevived = state;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public PlayerMovement GetPlayerScript()
    {
        return playerScript;
    }

    public void SetDeathPos(Vector3 pos)
    {
        deathPosition = pos;
    }
    #endregion
}
