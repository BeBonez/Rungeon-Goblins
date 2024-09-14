using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI components")]
    [SerializeField] GameObject gameOver;
    [SerializeField] TMP_Text coinHUD;

    [Header("Variables: ")]
    [SerializeField] GameObject player;
    [SerializeField] int coins;

    private void Awake()
    {
        coins = 0;
        coinHUD.text = "$" + coins.ToString();
    }

    public void GameOver ()
    {
        StopAllCoroutines();
        Time.timeScale = 0.0f;
        gameOver.SetActive(true);
        player.transform.Rotate(0, 0, 90);
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        coinHUD.text = "$" + coins.ToString();
    }
}
