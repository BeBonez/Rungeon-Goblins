using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI components")]
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject revivePanel;
    [SerializeField] TMP_Text reviveCostText;
    [SerializeField] TMP_Text reviveTimer;
    [SerializeField] TMP_Text[] coinHUD; // Todos os textos de Coins
    [SerializeField] TMP_Text[] distanceHUD; // Todos os textos de Distancia
    public Slider timeHud;

    [Header("Variables: ")]
    [SerializeField] GameObject player;
    [SerializeField] PlayerMovement playerScript;
    [SerializeField] int coins;
    [SerializeField] int quickReviveTime;
    private int actualQuickReviveTime;
    private int distance;
    private int reviveCost = 50;
    private Timer timer;
    private bool hasRevived = false;

    private void Awake()
    {
        UpdateData();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
        playerScript.gameManager = this;
        timer = GetComponent<Timer>();
    }

    public void GameOver ()
    {
        revivePanel.SetActive(false);
        StopAllCoroutines();
        UpdateData();
        player.transform.Rotate(0, 0, 90);
        Time.timeScale = 0.0f;
        losePanel.SetActive(true);
    }

    public IEnumerator ReviveQuickTime()
    {
        Time.timeScale = 0f;
        actualQuickReviveTime = quickReviveTime;
        reviveCostText.text = "REVIVE $" + reviveCost;
        playerScript.SwitchCanMove();
        revivePanel.SetActive(true);

        for (int i = actualQuickReviveTime; i > 0;i--)
        {
            reviveTimer.text = actualQuickReviveTime.ToString();
            actualQuickReviveTime--;
            yield return new WaitForSecondsRealtime(1);
        }

        if (hasRevived == false)
        {
            GameOver();
        }        
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        UpdateData();
    }

    public void AddDistance(int amount)
    {
        distance += amount;
        UpdateData();
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
    }

    public void Revive()
    {
        revivePanel.SetActive(false);
        coins -= reviveCost;
        reviveCost += 50;
        UpdateData();
        player.transform.position = playerScript.GetLastPosition();
        playerScript.UpdatePosition();
        playerScript.SwitchCanMove();
        timer.AddTime(timer.GetMaxTime());
        Time.timeScale = 1f;
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

    #endregion
}
