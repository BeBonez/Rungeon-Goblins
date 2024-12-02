using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void Imortal()
    {
        gameManager.GetPlayerScript().ToggleImortality();
    }

    public void Time()
    {
        gameManager.GetPlayerScript().ToggleInfTime();
    }

    public void Power()
    {
        gameManager.GetPlayerScript().ToggleInfPower();
    }
}
