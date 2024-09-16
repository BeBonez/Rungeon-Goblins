using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator200 : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private GameManager gameManager;
    private int spawnPosition;
    private int spawnGoal;
    private int despawnGoal;
    private int current;

    private void Start()
    {
        spawnPosition = 500;
        spawnGoal = current + 250;
        despawnGoal = current + 750;
    }

    private void FixedUpdate()
    {
        if (current >= spawnGoal)
        {
            GenerateNewTile();
        }
    }
    public void GenerateNewTile()
    {
        Instantiate(tile);
        spawnGoal += spawnGoal + 250;
    }
}
