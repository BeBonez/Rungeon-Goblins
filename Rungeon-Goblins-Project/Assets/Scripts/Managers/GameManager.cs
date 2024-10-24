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
    [SerializeField] Image reviveBackground;
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
    private Vector3 deathPosition;

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
        coinHUD[0].text = "$" + PlayerPrefs.GetInt("Money", 0);
        coins = PlayerPrefs.GetInt("Money", 0);
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

    public void GameOver ()
    {
        revivePanel.SetActive(false);
        StopAllCoroutines();
        UpdateData();
        Pause();
        losePanel.SetActive(true);
    }

    public IEnumerator ReviveQuickTime()
    {
        Pause();
        actualQuickReviveTime = quickReviveTime;
        reviveCostText.text = "REVIVE $" + reviveCost;
        revivePanel.SetActive(true);

        for (int i = actualQuickReviveTime; i > 0;i--)
        {
            reviveTimer.text = actualQuickReviveTime.ToString();
            actualQuickReviveTime--;
            reviveBackground.color = new Color(reviveBackground.color.r, reviveBackground.color.g, reviveBackground.color.b, 0.15f * i);
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
        int bestDistance = PlayerPrefs.GetInt("PersonalBest");

        if (distance > bestDistance)
        {
            PlayerPrefs.SetInt("PersonalBest", distance);
        }
        
        PlayerPrefs.SetInt("Money", coins);
    }

    public void Revive()
    {
        revivePanel.SetActive(false);
        timer.GetUIComponents().SetActive(true);

        coins -= reviveCost;
        reviveCost += 50;

        UpdateData();

        Vector3 newPos = new Vector3(deathPosition.x, deathPosition.y, deathPosition.z - 20);

        playerScript.SetNextPosition(newPos);
        player.transform.position = newPos;

        AddDistance(-2);

        playerScript.UpdatePosition();
        player.GetComponent<Animator>().Play("Idle");

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
