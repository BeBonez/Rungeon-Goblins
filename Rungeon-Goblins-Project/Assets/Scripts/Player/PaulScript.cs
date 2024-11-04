using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaulScript : PlayerMovement
{
    [Header("Dash")]

    [Header("Shield")]
    [SerializeField] float timeActive;
    float actualShieldTimeCooldown;
    bool isShieldCoolingDown;
    bool isActive;
    IEnumerator powerCoroutine;
    IEnumerator rechargeCoroutine;
    [SerializeField] float timeBetweenDashes;
    [SerializeField] float actualTime;

    [Header("UI Components")]
    [SerializeField] protected Image uiShield;

    private void Start()
    {
        charName = "Paul";

        nextposition = transform.position;

        uiMaxCharges = GameObject.Find("PaulMaxCharges").GetComponent<TMP_Text>();

        uiActualCharges = GameObject.Find("PaulActualCharges").GetComponent<TMP_Text>();

        uiShield = GameObject.Find("ShieldImage").GetComponent<Image>();

        animator = GetComponent<Animator>();

        timer = GameObject.Find("GameManager").GetComponent<Timer>();

        UpdatePosition();
    }

    private void FixedUpdate()
    {
        MoveToDestiny();

        if (isActive)
        {
            if (Time.time > timeBetweenDashes + actualTime)
            {
                Dash(new Vector3(0, 0, moveDistance));
                gameManager.AddDistance(1);
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
                if (isShieldCoolingDown == false && isActive == false)
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
                if (!(transform.position.x >= 10) && (moveDistance + nextposition.x <= 10)) // Se não for ultrapassar o limite da direita, ele pode mover
                {
                    Dash(new Vector3(moveDistance, 0, moveDistance));
                    gameManager.AddDistance(1);
                }

            }

            else if (direction.x <= -1)
            {
                if (!(transform.position.x <= -10) && (nextposition.x - moveDistance >= -10)) // Se não for ultrapassar o limite da esquerda, ele pode mover
                {
                    Dash(new Vector3(-moveDistance, 0, moveDistance));
                    gameManager.AddDistance(1);
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
        isActive = true;
        uiShield.color = Color.blue;
        canTakeDamge = false;

        yield return new WaitForSeconds(timeActive);

        isActive = false;
        canTakeDamge = true;
        isShieldCoolingDown = true;
        animator.Play("Idle");
        animator.SetTrigger("Idle");
        uiShield.color = Color.red;

        yield return new WaitForSeconds(powerCooldown);

        isShieldCoolingDown = false;
        uiShield.color = Color.white;
        StopCoroutine(powerCoroutine);
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

        if (isActive == false)
        {
            animator.Rebind();
            animator.Play("Jump");
        }

    }

    public override void DeactivatePower()
    {
        isActive = false;
    }
}
