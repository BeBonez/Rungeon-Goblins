using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
    [SerializeField] GameObject[] popUps;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Timer timer;
    [SerializeField] float timeBetweenText;
    [SerializeField] int popUpIndex;
    private float waitTime;
    private bool canMove = true;
    private bool timeTrigger;
    private Vector2 direction;

    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        timer.SetReduceTime(false);
        waitTime = timeBetweenText;
    }

    // Update is called once per frame
    void Update()
    {        
        for (int i = 0; i < popUps.Length; i++)
        {
            if (popUpIndex < popUps.Length)
            {
                if (i == popUpIndex)
                {
                    if (timeTrigger == false)
                    {
                        popUps[i].SetActive(true);
                        Time.timeScale = 0.3f;
                    }
                }
            }
            //else
            //{
            //    popUps[i].SetActive(false);
            //    Debug.Log("Desativado: " + i);
            //}
        }

        if (timeTrigger == true)
        {
            Time.timeScale = 1f;
            popUps[popUpIndex].SetActive(false);

            if (waitTime <= 0)
            {
                popUpIndex++;
                timeTrigger = false;
                waitTime = timeBetweenText;
                canMove = true;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (canMove == true)
        {
            if (popUpIndex == 0 && direction.x >= 1)
            {
                DoAction(direction);
            }
            else if (popUpIndex == 1 && direction.x <= -1)
            {
                DoAction(direction);
            }
            else if (popUpIndex == 2 && direction.y >= 1)
            {
                DoAction(direction);
            }
            else if (popUpIndex == 3 && direction.x >= 1 || popUpIndex == 3 && direction.x <= -1)
            {
                DoAction(direction);
            }
            else if (popUpIndex == 4 && direction.y >= 1)
            {
                DoAction(direction);
            }
            else if (popUpIndex == 5 && direction.y <= -1)
            {
                DoAction(direction);
            }
            else if (popUpIndex == 6 && playerMovement)
            {

            }
        }
    }

    private void DoAction(Vector2 direction)
    {
        playerMovement.Move(direction);
        canMove = false;
        direction.Set(0, 0);
        timeTrigger = true;
    }

    public void Direction(Vector2 direction)
    {
        //Debug.Log("Legal");
        this.direction = direction;
    }
}
