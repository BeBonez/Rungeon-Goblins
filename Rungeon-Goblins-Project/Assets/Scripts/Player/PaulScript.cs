using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PaulScript : PlayerMovement
{
    [Header("Massacre")]
    [NonSerialized] public GameObject nextGoblin;
    [SerializeField] float speedMultiplier;
    IEnumerator powerCoroutine, rechargeCoroutine;

    [Header("UI Components")]
    protected Image uiShield;

    private void Start()
    {
        charName = "Paul";

        originalSpeed = speed;

        nextposition = transform.position;

        uiMaxCharges = GameObject.Find("PaulMaxCharges").GetComponent<TMP_Text>();

        uiActualCharges = GameObject.Find("PaulActualCharges").GetComponent<TMP_Text>();

        uiShield = GameObject.Find("ShieldImage").GetComponent<Image>();

        animator = GetComponent<Animator>();

        timer = GameObject.Find("GameManager").GetComponent<Timer>();

        originalKillMeta = killMeta;

        powerBar = gameManager.GetComponent<PowerBar>();

        powerBar.SetMaximumFill(originalKillMeta);

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            actualKills = originalKillMeta - 2;
            powerBar.SetCurrentFill(actualKills);
        }

        powerBar.SetCurrentFill(actualKills);

        UpdatePosition();
    }

    private void Update()
    {
        MoveToDestiny();

        if (isPowerActive == true)
        {
            nextGoblin = GetClosest("Goblin");

            if (nextGoblin != null)
            {
                if (kills < maxKills)
                {
                    if (killed)
                    {
                        if (Time.time > timeBetweenDashes + actualTime)
                        {
                            actualTime = Time.time;
                            DashToGoblin();
                        }

                    }

                }

                else if (kills >= maxKills && killed == true)
                {
                    DeactivatePower();
                }
            }

        }

        UpdateUIInfo();
    }

    public override void Move(Vector2 direction)
    {
        if (canMove)
        {

            if (isPowerActive == false)
            {
                if (direction.y >= 1)
                {

                    if (actualCharges > 0)
                    {
                        Dash(new Vector3(0, 0, moveDistance));

                        actualCharges--;
                        gameManager.AddDistance(1);
                    }

                }
                else if (direction.y <= -1)
                {
                    if (CanUsePower() && isPowerActive == false)
                    {
                        // animator.Play("Run");
                        // animator.SetTrigger("Run");
                        powerCoroutine = RaiseShield();
                        StartCoroutine(powerCoroutine);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX(11);
                    }
                }

                else if (direction.x >= 1)
                {
                    if (!(transform.position.x >= 10) && (moveDistance + nextposition.x <= 10)) // Se n�o for ultrapassar o limite da direita, ele pode mover
                    {
                        Dash(new Vector3(moveDistance, 0, moveDistance));
                        gameManager.AddDistance(1);
                    }

                }

                else if (direction.x <= -1)
                {
                    if (!(transform.position.x <= -10) && (nextposition.x - moveDistance >= -10)) // Se n�o for ultrapassar o limite da esquerda, ele pode mover
                    {
                        Dash(new Vector3(-moveDistance, 0, moveDistance));
                        gameManager.AddDistance(1);
                    }
                }
            }
            UpdatePosition();
        }

        else
        {
            return;
        }

    }

    private IEnumerator RaiseShield()
    {
        killed = true;

        isPowerActive = true;

        speed *= speedMultiplier;

        yield return new WaitForSeconds(timeActive);

        //DeactivatePower();

        if (timer.IsDead() == false)
        {
            animator.Play("Idle");
            animator.SetTrigger("Idle");
        }

    }

    public override void DeactivatePower()
    {
        ResetPowerFill();

        kills = 0;

        speed = originalSpeed;

        isPowerActive = false;

        animator.Play("Idle");
    }

    protected override void Dash(Vector3 direction)
    {

        if (hasReached == false)
        {
            transform.position = nextposition;

            lastPosition = nextposition;

            direction += transform.position;

            nextposition = direction;
        }
        else
        {
            direction += transform.position;

            lastPosition = transform.position;

            nextposition = direction;

        }

        AudioManager.Instance.PlaySFX(9);

        if (isPowerActive == false)
        {
            animator.Rebind();
            animator.Play("Jump");
        }

    }

    private void DashToGoblin()
    {
        kills++;

        animator.Rebind();

        animator.Play("Jump");

        Debug.Log("Kill:" + kills);

        nextposition = nextGoblin.transform.position;

        float distanceToAdd = (nextGoblin.transform.position.z - transform.position.z) / 10;

        UpdatePosition();

        gameManager.AddDistance((int)distanceToAdd);

        killed = false;
    }
    public GameObject GetClosest(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        GameObject closestObject = null;

        float nearestDistance = 10000;

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].transform.position.z - transform.position.z >= 0)
            {
                float distance = Vector3.Distance(transform.position, objects[i].transform.position);

                if (distance < nearestDistance)
                {
                    closestObject = objects[i];
                    nearestDistance = distance;
                }
            }
        }

        return closestObject;
    }
}
