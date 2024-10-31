using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timer = 0;
    [SerializeField] private float maxTimer = 0;
    [SerializeField] private float LeastMaxTimer = 0;
    [SerializeField] private int reduceTimeDistance = 0;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Slider timeHUD;
    [SerializeField] private Animator animator;
    private bool canReduce = true;
    private Transform holePosition;
    private GameObject uiComponents;
    private IEnumerator reviveCoroutine;
    private bool isDead = false;
    private bool isDeadByHole = false;
    private int currentDistance;


    void Start()
    {
        timer = maxTimer;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiComponents = GameObject.Find("UI");
    }

    void FixedUpdate()
    {
        if (timer > 0)
        {

            if (canReduce)
            {
                timer -= Time.deltaTime;
            }
            
            timeHUD.value = timer / maxTimer;
        }

        if (timer > LeastMaxTimer)
        {
            currentDistance = gameManager.GetDistance();

            if (currentDistance > reduceTimeDistance)
            {
                maxTimer--;
                reduceTimeDistance += 25;
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

        // se tempo acabar chama método e perde
        if (timer <= 0 && isDead == false)
        {
            isDead = true;
            reviveCoroutine = gameManager.ReviveQuickTime();
            if (isDeadByHole == true)
            {
                isDeadByHole = false;
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

    public void DeadByRole(GameObject hole)
    {
        isDeadByHole = true;
        holePosition = hole.transform;
    }

    private IEnumerator Death(string _case)
    {

        uiComponents.SetActive(false);

        Animator playerAnim = gameManager.GetPlayer().GetComponent<Animator>();

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
            waitSeconds = 1f;
        }

        yield return new WaitForSecondsRealtime(waitSeconds);

        StartCoroutine(reviveCoroutine);
    }

    public void SetReduceTime(bool condition)
    {
        canReduce = condition;
    }
}
