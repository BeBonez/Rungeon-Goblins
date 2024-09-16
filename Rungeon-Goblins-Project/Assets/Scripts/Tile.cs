using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameManager gameManager;
    private int despawnGoal;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetDespawnGoal(int whenToDespawn)
    {
        despawnGoal = whenToDespawn;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.GetDistance() >= despawnGoal) 
        { 
            Destroy(gameObject);
        }
    }
}
