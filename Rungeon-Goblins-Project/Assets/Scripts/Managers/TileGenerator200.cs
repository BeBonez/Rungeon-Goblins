using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator200 : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameManager gameManager;
    private Vector3 originalPosition;
    private int current;
    private int spawnPosition;
    private int spawnGoal;
    private int despawnGoal;
    private bool secondSpawn;

    private void Awake()
    {
        originalPosition = tilePrefab.transform.position;
        spawnPosition = 0;
        despawnGoal = 30;
        GenerateNewTile();
    }

    private void FixedUpdate()
    {
        current = gameManager.GetDistance();

        if (secondSpawn == false)
        {
            if (current >= 10)
            {
                spawnGoal = 10;
                GenerateNewTile();
                secondSpawn = true;
            }
        }

        if (current >= spawnGoal)
        {
            GenerateNewTile();
        }

    }
    public void GenerateNewTile()
    {
        Vector3 nextPostion = new Vector3(originalPosition.x, originalPosition.y, spawnPosition);
        GameObject newTile = Instantiate(tilePrefab, nextPostion, tilePrefab.transform.rotation);
        newTile.GetComponent<Tile>().SetDespawnGoal(despawnGoal);
        Debug.Log("Criou");
        spawnGoal += 25;
        spawnPosition += 250;
        despawnGoal += 25;
    }
}
