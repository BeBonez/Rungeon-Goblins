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

    private int currentDistance;
    

    void Start()
    {
        timer = maxTimer;
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        timeHUD.value = timer / maxTimer;

        if (timer > LeastMaxTimer)
        {
            currentDistance = gameManager.GetDistance();

            if (currentDistance > reduceTimeDistance) 
            {
                maxTimer--;
                reduceTimeDistance += 25;
            }
        }

        // se tempo acabar chama m�todo e perde
        if (timer <= 0)
        {
            gameManager.GameOver();
        }
    }

    public void AddTime(float amount)
    {
        timer += amount;
        if (timer > maxTimer)
        {
            timer = maxTimer;
        }
    }

    public float GetMaxTime()
    {
        return maxTimer;
    }
}
