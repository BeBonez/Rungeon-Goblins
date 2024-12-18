using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timer = 0, maxTimer = 0, LeastMaxTimer = 0;
    [SerializeField] private int reduceTimeDistance = 0;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Slider timeHUD;
    [SerializeField] private Animator animator;
    private bool infTime;
    private bool canReduce = true;
    private Transform holePosition;
    private GameObject uiComponents;
    private IEnumerator reviveCoroutine;
    private bool isDead = false;
    private bool isDeadByHole = false;
    private int currentDistance;


    void Start()
    {
        timer = timeHUD.value * maxTimer;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiComponents = GameObject.Find("UI");
    }

    void Update()
    {
        if (infTime == false)
        {
            if (timer > 0)
            {

                if (canReduce)
                {
                    timer -= Time.deltaTime;
                }
                
                timeHUD.value = timer / maxTimer;
            }
        }

        if (timer > LeastMaxTimer)
        {
            currentDistance = gameManager.GetDistance();

            if (currentDistance > reduceTimeDistance)
            {
                maxTimer--;
                reduceTimeDistance += 25; // Balancear reduzir tempo
            }
        }

        if (timer <= maxTimer * 0.3f) // Se for menor doq 30%
        {
            animator.SetTrigger("TimeEnding");
        }
        else
        {
            animator.ResetTrigger("TimeEnding");
            animator.ResetTrigger("Default");
            animator.SetTrigger("Default");
        }

        // se tempo acabar chama m�todo e perde
        if (timer <= 0 && isDead == false)
        {
            isDead = true;
            reviveCoroutine = gameManager.ReviveQuickTime();
            if (isDeadByHole == true)
            {
                isDeadByHole = false;
                gameManager.GetPlayerScript().SetNextPosition(holePosition.position);
                StartCoroutine(Death("Hole"));
            }
            else
            {
                StartCoroutine(Death("Normal"));
            }
        }
    }

    public void Revive()
    {
        if (gameManager.CanRevive())
        {
            StopCoroutine(reviveCoroutine);
            gameManager.SetRevived(true);
            gameManager.Revive();
            gameManager.SetRevived(false);
            isDead = false;
        }
        else
        {
            // Faz um beem e pisca vermelho as moedas
        }

    }

    public void AddTime(float amount)
    {
        timer += amount;
        if (timer > maxTimer)
        {
            timer = maxTimer;
        }
        else if (timer < 0)
        {
            timer = 0;
        }
    }

    public float GetMaxTime()
    {
        return maxTimer;
    }

    public GameObject GetUIComponents()
    {
        return uiComponents;
    }

    public void DeadByHole(GameObject hole)
    {
        isDeadByHole = true;
        holePosition = hole.transform;
        gameManager.GetPlayerScript().SetNextPosition(holePosition.position);
    }

    private IEnumerator Death(string _case)
    {

        uiComponents.SetActive(false);

        Animator playerAnim = gameManager.GetPlayerScript().GetAnimator();

        gameManager.GetPlayerScript().SwitchCanMove(false);

        gameManager.GetPlayerScript().DeactivatePower();

        float waitSeconds = 0;

        if (_case == "Normal")
        {
            gameManager.SetDeathPos(gameManager.GetPlayerScript().GetNextPosition());
            playerAnim.Play("Death");
            waitSeconds = 2.5f;
        }
        else if (_case == "Hole")
        {
            gameManager.SetDeathPos(holePosition.position);
            playerAnim.Play("Falling");
            waitSeconds = 3.5f;
        }

        yield return new WaitForSecondsRealtime(waitSeconds);

        StartCoroutine(reviveCoroutine);
    }

    public void SetReduceTime(bool condition)
    {
        canReduce = condition;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool IsDeadByHole()
    {
        return isDeadByHole;
    }

    public void SetDead(bool condition)
    {
        isDead = condition;
    }

    public void SetInfTime(bool condition)
    {
        infTime = condition;
    }
}
