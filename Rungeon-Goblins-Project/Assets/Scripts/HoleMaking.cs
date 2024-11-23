using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMaking : MonoBehaviour
{
    private GameManager gameManager;
    private int currentDistance;

    [Header("Tiers")]
    [SerializeField] private int TierGoal; // 0 for area 1, 220 for area 2, 470 for area 3...
    [SerializeField] private bool DrillSecondTier;
    [SerializeField] private bool DrillThirdTier;
    private bool SecondTier;
    private bool ThirdTier;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDistance = gameManager.GetDistance();

        if (currentDistance > TierGoal + 110)
        {
            SecondTier = true;
        }

        if (currentDistance > TierGoal + 710)
        {
            ThirdTier = true;       
        }

        if (DrillSecondTier) 
        {
            if (SecondTier)
            {
                Destroy(gameObject);
            }
        }

        if (DrillThirdTier)
        {
            if (ThirdTier)
            {
                Destroy(gameObject);
            }
        }
    }
}
