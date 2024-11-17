using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PaulScript : PlayerMovement
{
    private float originalSpeed;

    [Header("Power")]
    public GameObject nextGoblin;
    [SerializeField] float speedMultiplier;
    [SerializeField] float timeActive;
    IEnumerator powerCoroutine;
    IEnumerator rechargeCoroutine;
    [SerializeField] float timeBetweenDashes;
    float actualTime;

    [Header("UI Components")]
    [SerializeField] protected Image uiShield;

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

        originalKillLimit = killLimit;

        powerBar = gameManager.GetComponent<PowerBar>();

        powerBar.SetMaximumFill(originalKillLimit);

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            actualKills = originalKillLimit - 2;
            powerBar.SetCurrentFill(actualKills);
        }

        powerBar.SetCurrentFill(actualKills);

        UpdatePosition();
    }

    private void Update()
    {
        MoveToDestiny();

        if (isFliyng == true)
        {
            if (Time.time > timeBetweenDashes + actualTime)
            {
                nextGoblin = GetClosest("Goblin");

                if (nextGoblin != null)
                {
                    nextGoblin.transform.Rotate(0, 90, 0);

                    nextposition = nextGoblin.transform.position;

                    float distanceToAdd = (nextGoblin.transform.position.z - transform.position.z) / 10;

                    gameManager.AddDistance((int)distanceToAdd);
                }

                actualTime = Time.time;
            }
        }

        //if (actualCharges < maxCharges && isDashCoolingDown == false)
        //{
        //    rechargeCoroutine = Recharge();
        //    StartCoroutine(rechargeCoroutine);
        //}

        UpdateUIInfo();
    }

    public override void Move(Vector2 direction)
    {
        if (canMove)
        {

            // Sem poder

            if (isFliyng == false)
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
                    if (CanUsePower() && isFliyng == false)
                    {
                        animator.Play("Run");
                        animator.SetTrigger("Run");
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

            //Com poder

            else
            {
                if (direction.x >= 1)
                {
                    if (!(transform.position.x >= 10) && (moveDistance + nextposition.x <= 10)) // Se n�o for ultrapassar o limite da direita, ele pode mover
                    {
                        Dash(new Vector3(moveDistance, 0, 0));
                        //nextposition = new Vector3(moveDistance + nextposition.x, nextposition.y , nextposition.z);
                        gameManager.AddDistance(1);
                    }

                }

                else if (direction.x <= -1)
                {
                    if (!(transform.position.x <= -10) && (nextposition.x - moveDistance >= -10)) // Se n�o for ultrapassar o limite da esquerda, ele pode mover
                    {
                        Dash(new Vector3(-moveDistance, 0, 0));
                        //nextposition = new Vector3(-moveDistance + nextposition.x, nextposition.y, nextposition.z);
                        gameManager.AddDistance(1);
                    }
                }
            }

        }

        UpdatePosition();
    }

    private IEnumerator Recharge()
    {
        isDashCoolingDown = true;
        yield return new WaitForSeconds(dashCoolDown);
        if (actualCharges < maxCharges)
        {
            actualCharges++;
            isDashCoolingDown = false;
            AudioManager.Instance.PlaySFX(7);
        }
        StopCoroutine(rechargeCoroutine);
    }

    private IEnumerator RaiseShield()
    {
        isFliyng = true;

        speed *= speedMultiplier;

        yield return new WaitForSeconds(timeActive);

        DeactivatePower();

        if (timer.IsDead() == false)
        {
            animator.Play("Idle");
            animator.SetTrigger("Idle");
        }

    }

    public override void DeactivatePower()
    {
        ResetPowerFill();
        speed = originalSpeed;
        isFliyng = false;
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

        if (isFliyng == false)
        {
            animator.Rebind();
            animator.Play("Jump");
        }

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
