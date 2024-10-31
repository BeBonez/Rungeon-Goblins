using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
    [SerializeField] GameObject[] popUps;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float waitTime;
    private Vector2 direction;
    private int popUpIndex;

    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        GameObject.Find("GameManager").GetComponent<Timer>().SetReduceTime(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);
            }
        }

        if (popUpIndex == 0 && direction.x >= 1)
        {
            playerMovement.Move(direction);
            direction.Set(0, 0);
            popUpIndex++;
        }
        else if (popUpIndex == 1 && direction.x <= -1)
        {
            playerMovement.Move(direction);
            direction.Set(0, 0);
            popUpIndex++;
        } 
        else if (popUpIndex == 2 && direction.y >= 1)
        {
            playerMovement.Move(direction);
            direction.Set(0, 0);
            popUpIndex++;
        }
        else if (popUpIndex == 3 && direction.y >= 1)
        {
            playerMovement.Move(direction);
            direction.Set(0, 0);
            popUpIndex++;
        }


    }

    public void Direction(Vector2 direction)
    {
        Debug.Log("Legal");
        this.direction = direction;
    }
}
