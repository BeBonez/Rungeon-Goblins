using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timer = 0;
    [SerializeField] private float maxTimer = 0;
    [SerializeField] private float LeastMaxTimer = 0;
    [SerializeField] private int reduceTimeDistance = 0;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Slider timeHUD;
    private IEnumerator reviveCoroutine;
    private bool isDead = false;

    private int currentDistance;
    

    void Start()
    {
        timer = maxTimer;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if( timer > 0 )
        {
            timer -= Time.deltaTime;
            timeHUD.value = timer / maxTimer;
        }

        if (timer > LeastMaxTimer)
        {
            currentDistance = gameManager.GetDistance();

            if (currentDistance > reduceTimeDistance) 
            {
                maxTimer--;
                reduceTimeDistance += 25;
            }
        }

        // se tempo acabar chama método e perde
        if (timer <= 0 && isDead == false)
        {
            isDead = true;
            reviveCoroutine = gameManager.ReviveQuickTime();
            StartCoroutine(reviveCoroutine);
        }
    }

    public void Revive()
    {
        if (gameManager.CanRevive())
        {
            StopCoroutine(reviveCoroutine);
            gameManager.SetRevived(true);
            gameManager.Revive();
            gameManager.SetRevived(false);
            isDead = false;
        }
        else
        {
            // Faz um beem e pisca vermelho as moedas
        }

    }

    public void AddTime(float amount)
    {
        timer += amount;
        if (timer > maxTimer)
        {
            timer = maxTimer;
        }
        else if (timer < 0)
        {
            timer = 0;
        }
    }

    public float GetMaxTime()
    {
        return maxTimer;
    }
}
