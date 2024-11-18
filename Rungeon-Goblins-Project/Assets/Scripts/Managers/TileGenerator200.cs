using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator200 : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header("Tiles")]
    [SerializeField] private GameObject[] tilePrefabs;
    private GameObject tilePrefab;

    private Vector3 originalPosition;
    private int currentDistance;
    private int spawnPosition;
    private int spawnGoal;
    private int despawnGoal;
    private bool secondSpawn; 

    private void Awake()
    {
        tilePrefab = tilePrefabs[Random.Range(0, 5)];
        originalPosition = tilePrefab.transform.position;
        spawnPosition = 0;
        despawnGoal = 30;
        GenerateNewTile();
    }

    private void FixedUpdate()
    {
        currentDistance = gameManager.GetDistance();

        if (secondSpawn == false)
        {
            if (currentDistance >= 10)
            {
                spawnGoal = 10;
                GenerateNewTile();
                secondSpawn = true;
            }
        }

        if (currentDistance >= spawnGoal)
        {
            GenerateNewTile();
        }

    }
    public void GenerateNewTile()
    {
        int random;

        while (currentDistance > 750) 
        {
            currentDistance -= 750;
        }

        if (currentDistance > 730)
        {
            random = Random.Range(0, 5);
        }
        else if (currentDistance > 470)
        {
            random = Random.Range(10, 15);
        } else if (currentDistance > 220) {
            random = Random.Range(5, 10);
        } else
        {
            random = Random.Range(0, 5);
        }

        tilePrefab = tilePrefabs[random];
        Vector3 nextPostion = new Vector3(originalPosition.x, originalPosition.y, spawnPosition);
        GameObject newTile = Instantiate(tilePrefab, nextPostion, tilePrefab.transform.rotation);
        newTile.GetComponent<Tile>().SetDespawnGoal(despawnGoal);
        spawnGoal += 25;
        spawnPosition += 250;
        despawnGoal += 25;
    }
}
