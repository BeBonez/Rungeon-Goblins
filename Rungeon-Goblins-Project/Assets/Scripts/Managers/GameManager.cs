using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI components")]
    [SerializeField] GameObject gameOver;
    [SerializeField] TMP_Text[] coinHUD; // Todos os textos de Coins
    [SerializeField] TMP_Text[] distanceHUD; // Todos os textos de Distancia
    public Slider timeHud;

    [Header("Variables: ")]
    [SerializeField] GameObject player;
    [SerializeField] int coins;
    private int distance;

    private void Awake()
    {
        UpdateData();
        coins = 0;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().gameManager = this;
    }

    public void GameOver ()
    {
        StopAllCoroutines();
        UpdateData();
        player.transform.Rotate(0, 0, 90);
        Time.timeScale = 0.0f;
        gameOver.SetActive(true);
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
    public int GetDistance()
    {
        return distance;
    }

    #region getsSets

    #endregion
}
