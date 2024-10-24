using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaulScript : PlayerMovement
{
    [Header("Dash")]
    [SerializeField] int maxCharges;
    [SerializeField] int actualCharges;
    [SerializeField] float dashCoolDown;
    [SerializeField] bool isDashCoolingDown;

    [Header("Shield")]
    [SerializeField] float timeActive;
    [SerializeField] float shieldCoolDown;
    float actualShieldTimeCooldown;
    bool isShieldCoolingDown;
    bool isActive;
    IEnumerator powerCoroutine;
    IEnumerator rechargeCoroutine;

    [Header("UI Components")]
    [SerializeField] protected TMP_Text uiMaxCharges;
    [SerializeField] protected TMP_Text uiActualCharges;
    [SerializeField] protected Image uiShield;

    private void Start()
    {
        charName = "Paul";
        nextposition = transform.position;
        uiMaxCharges = GameObject.Find("MaxCharges").GetComponent<TMP_Text>();
        uiActualCharges = GameObject.Find("ActualCharges").GetComponent<TMP_Text>();
        uiShield = GameObject.Find("ShieldImage").GetComponent<Image>();
        animator = GetComponent<Animator>();

        UpdatePosition();
    }

    private void FixedUpdate()
    {
        MoveToDestiny();

        if (actualCharges < maxCharges && isDashCoolingDown == false)
        {
            rechargeCoroutine = Recharge();
            StartCoroutine(rechargeCoroutine);
        }

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
                    powerCoroutine = RaiseShield();
                    StartCoroutine(powerCoroutine);
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
        actualCharges++;
        isDashCoolingDown = false;
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
        uiShield.color = Color.red;

        yield return new WaitForSeconds(shieldCoolDown);
        isShieldCoolingDown = false;
        uiShield.color = Color.white;
        StopCoroutine(powerCoroutine);
    }

    private void UpdateUIInfo()
    {
        uiActualCharges.text = actualCharges.ToString();
        uiMaxCharges.text = maxCharges.ToString();
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
    }
}
