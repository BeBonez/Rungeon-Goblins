using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GarryScript : PlayerMovement
{
    [SerializeField] float activeTime;
    [SerializeField] bool isMagicActive = false;
    [SerializeField] float timeBetweenDashes;
    [SerializeField] float actualTime;
    bool isMagicCoolingDown;
    IEnumerator magicCoroutine;

    [Header("UI Components")]
    [SerializeField] protected Image uiPower;

    private void Start()
    {
        charName = "Garry";

        nextposition = transform.position;

        uiPower = GameObject.Find("MagicImage").GetComponent<Image>();

        uiMaxCharges = GameObject.Find("GarryMaxCharges").GetComponent<TMP_Text>();

        uiActualCharges = GameObject.Find("GarryActualCharges").GetComponent<TMP_Text>();

        animator = gameObject.GetComponent<Animator>();

        timer = GameObject.Find("GameManager").GetComponent<Timer>();

        UpdatePosition();
    }

    private void FixedUpdate()
    {
        MoveToDestiny();

        UpdateUIInfo();

        if (isFliyng == true)
        {
            if (Time.time > timeBetweenDashes + actualTime)
            {
                Dash(new Vector3(0, 0, moveDistance));
                gameManager.AddDistance(1);
                actualTime = Time.time;

            }
        }

        if (transform.position.x >= 12)
        {
            nextposition = new Vector3(-10, 0, nextposition.z);

            transform.position = nextposition;
            AudioManager.Instance.PlaySFX(8);
        }

        if (transform.position.x <= -12)
        {
            nextposition = new Vector3(10, 0, nextposition.z);

            transform.position = nextposition;
            AudioManager.Instance.PlaySFX(8);
        }
    }

    public override void Move(Vector2 direction)
    {
        if (canMove)
        {

            // Sem poder

            if (isMagicActive == false)
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
                else if (direction.y <= -1 && posZ != -5)
                {
                    if (isMagicActive == false && isMagicCoolingDown == false)
                    {
                        Dash(new Vector3(0, 0, -moveDistance));
                        UpdatePosition();

                        gameManager.AddDistance(-1);

                        magicCoroutine = TeleportMagic();
                        StartCoroutine(magicCoroutine);
                        animator.Play("Flying");
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX(11);
                    }
                }

                else if (direction.x >= 1)
                {

                    Dash(new Vector3(moveDistance, 0, moveDistance));

                    gameManager.AddDistance(1);

                }

                else if (direction.x <= -1)
                {

                    Dash(new Vector3(-moveDistance, 0, moveDistance));

                    gameManager.AddDistance(1);
                }
            }

            // Com poder

            else
            {
                if (direction.y >= 1)
                {
                    //Dash(new Vector3(0, 0, moveDistance));
                    //gameManager.AddDistance(1);

                }
                else if (direction.y <= -1)
                {
                    AudioManager.Instance.PlaySFX(11);
                }

                else if (direction.x >= 1)
                {
                    Dash(new Vector3(moveDistance, 0, 0));

                }

                else if (direction.x <= -1)
                {
                    Dash(new Vector3(-moveDistance, 0, 0));

                }
            }

        }

        UpdatePosition();

    }

    private IEnumerator TeleportMagic()
    {
        AudioManager.Instance.PlaySFX(6);

        isMagicActive = true;

        isFliyng = true;

        uiPower.color = Color.blue;

        posY += 3; //PH

        canTakeDamge = false;

        transform.position = new Vector3(posX, posY, posZ); //PH

        yield return new WaitForSeconds(activeTime);

        AudioManager.Instance.StopSFX(6);

        isMagicActive = false;

        isFliyng = false;

        canTakeDamge = true;

        uiPower.color = Color.red;

        isMagicCoolingDown = true;

        animator.Play("Idle");

        posY -= 3; //PH

        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;

        UpdatePosition();

        transform.position = new Vector3(posX, posY, posZ); //PH

        yield return new WaitForSeconds(powerCooldown);

        uiPower.color = Color.white;

        AudioManager.Instance.PlaySFX(7);

        isMagicCoolingDown = false;

        StopCoroutine(magicCoroutine);
    }

    public bool IsMagicActive() { return isMagicActive; }

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
            lastPosition = transform.position;

            if (timer.IsDead() == false)
            {
                direction += transform.position;

                nextposition = direction;
            }

            UpdatePosition();

            if (isFliyng == false)
            {
                AudioManager.Instance.PlaySFX(9);
                animator.Rebind();
                animator.Play("Jump");
            }
        }
    }

    public override void DeactivatePower()
    {
        isFliyng = false;
        isMagicActive = false;
    }
}