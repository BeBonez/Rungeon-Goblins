using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator200 : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameManager gameManager;
    private int current;
    private int spawnPosition;
    private int spawnGoal;
 
    private void Start()
    {
        spawnPosition = 0;
        spawnGoal = current + 250;
    }

    private void FixedUpdate()
    {
        current = gameManager.GetDistance(); 

        if (current >= spawnGoal)
        {
            GenerateNewTile();
            spawnGoal += 500;
        }
    }
    public void GenerateNewTile()
    {
        GameObject newTile = Instantiate(tilePrefab);
        newTile.GetComponent<Tile>().SetDespawnGoal(current + 750);
        spawnGoal += spawnGoal + 250;
    }
}
