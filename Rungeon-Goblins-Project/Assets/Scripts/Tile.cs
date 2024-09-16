using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameManager gameManager;
    private int despawnGoal;

    private void Start()
    {
        GameObject.Find("GameManager");
    }

    public void SetDespawnGoal(int whenToDespawn)
    {
        despawnGoal = whenToDespawn;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (0 > despawnGoal) 
        { 
        
        }
    }
}
