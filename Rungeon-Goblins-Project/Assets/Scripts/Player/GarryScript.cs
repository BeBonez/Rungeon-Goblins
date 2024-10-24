using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GarryScript : PlayerMovement
{
    [SerializeField] float activeTime;
    [SerializeField] float magicCooldown;
    [SerializeField] bool isMagicActive = false;
    bool isMagicCoolingDown;
    IEnumerator magicCoroutine;

    [Header("UI Components")]
    [SerializeField] protected Image uiPower;

    private void Start()
    {
        charName = "Garry";
        nextposition = transform.position;
        uiPower = GameObject.Find("MagicImage").GetComponent<Image>();
        animator = gameObject.GetComponent<Animator>();
        UpdatePosition();
    }

    private void FixedUpdate()
    {
        MoveToDestiny();

        if (transform.position.x >= 12)
        {
            nextposition = new Vector3(-10, 0, nextposition.z);
            transform.position = nextposition;
        }

        if (transform.position.x <= -12)
        {
            nextposition = new Vector3(10, 0, nextposition.z);
            transform.position = nextposition;
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
                    // BEEm

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
                    Dash(new Vector3(0, 0, moveDistance));
                    gameManager.AddDistance(1);

                }
                else if (direction.y <= -1)
                {
                    //Beem
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

        }

        UpdatePosition();

    }

    private IEnumerator TeleportMagic()
    {
        isMagicActive = true;

        isFliyng = true;

        uiPower.color = Color.blue;

        posY += 3; //PH

        transform.position = new Vector3(posX, posY, posZ); //PH

        yield return new WaitForSeconds(activeTime);

        isMagicActive = false;

        isFliyng = false;

        uiPower.color = Color.red;

        isMagicCoolingDown = true;

        animator.Play("Idle");

        posY -= 3; //PH

        UpdatePosition();

        transform.position = new Vector3(posX, posY, posZ); //PH

        yield return new WaitForSeconds(magicCooldown);

        uiPower.color = Color.white;

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

            direction += transform.position;

            nextposition = direction;

        }
    }
}