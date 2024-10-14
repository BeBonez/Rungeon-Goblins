using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameManager gameManager;
    private int despawnGoal;
    private int currentDistance;

    [Header("Tiers")]
    [SerializeField] private int TierGoal; // 0 for area 1, 240 for area 2, 490 for area 3...
    [SerializeField] private GameObject[] SecondTier;
    [SerializeField] private GameObject[] ThirdTier;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDistance = gameManager.GetDistance();

        if (currentDistance > TierGoal + 120)
        {
            for (int i = 0; i < SecondTier.Length; i++)
            {
                SecondTier[i].SetActive(true);
            }
            
        }

        if (currentDistance > TierGoal + 740)
        {
            for (int i = 0; i < SecondTier.Length; i++)
            {
                ThirdTier[i].SetActive(true);
            }

        }

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
