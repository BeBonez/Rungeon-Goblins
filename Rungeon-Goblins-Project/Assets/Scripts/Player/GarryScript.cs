using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GarryScript : PlayerMovement
{
    bool dashed;
    IEnumerator magicCoroutine;

    [Header("UI Components")]
    protected Image uiPower;

    private void Start()
    {
        charName = "Garry";

        originalSpeed = speed;

        nextposition = transform.position;

        uiPower = GameObject.Find("MagicImage").GetComponent<Image>();

        uiMaxCharges = GameObject.Find("GarryMaxCharges").GetComponent<TMP_Text>();

        uiActualCharges = GameObject.Find("GarryActualCharges").GetComponent<TMP_Text>();

        //animator = gameObject.GetComponent<Animator>();

        timer = GameObject.Find("GameManager").GetComponent<Timer>();

        originalKillMeta = killMeta;

        powerBar = gameManager.GetComponent<PowerBar>();

        powerBar.SetMaximumFill(originalKillMeta);

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            actualKills = originalKillMeta - 2;
        }

        powerBar.SetCurrentFill(actualKills);

        UpdatePosition();
    }

    private void Update()
    {
        MoveToDestiny();

        if (isPowerActive == true)
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
            dashedThroughWall = 1;
            nextposition = new Vector3(-10, 0, nextposition.z);

            transform.position = nextposition;
            AudioManager.Instance.PlaySFX(8);
        }

        if (transform.position.x <= -12)
        {
            dashedThroughWall = 1;
            nextposition = new Vector3(10, 0, nextposition.z);

            transform.position = nextposition;
            AudioManager.Instance.PlaySFX(8);
        }

        UpdateUIInfo();
    }

    public override void Move(Vector2 direction)
    {
        if (canMove)
        {

            // Sem poder

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
                    if (isPowerActive == false && CanUsePower())
                    {
                        //Dash(new Vector3(0, 0, -moveDistance));
                        UpdatePosition();

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
        trail.SetActive(true);

        AudioManager.Instance.PlaySFX(6);

        isPowerActive = true;

        posY += 7;

        transform.position = new Vector3(posX, posY, posZ); //PH

        yield return new WaitForSeconds(timeActive);

        if (timer.IsDead() == false)
        {
            animator.Play("Idle");
        }

        DeactivatePower();

        posY -= 7; //PH

        CapsuleCollider collider = GetComponent<CapsuleCollider>();

        collider.enabled = false;
        collider.enabled = true;

        UpdatePosition();

        transform.position = new Vector3(posX, posY, posZ); //PH
    }
    protected override void Dash(Vector3 direction)
    {
        Vector3 particleSpawn = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        GameObject dashParticle = Instantiate(dashFx, particleSpawn, Quaternion.identity);
        Destroy(dashParticle, 3f);

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

        nextposition = new Vector3((float)Math.Floor(nextposition.x), (float)Math.Floor(nextposition.y), (float)Math.Floor(nextposition.z));

        nextposition = FixPosition(nextposition);
        
        if (isPowerActive == false)
        {
            AudioManager.Instance.PlaySFX(9);
        }

        if (isPowerActive == false)
        {
            animator.Rebind();
            animator.Play("Jump");
        }

    }

    public override void DeactivatePower()
    {
        trail.SetActive(false);
        AudioManager.Instance.StopSFX(6);
        ResetPowerFill();
        isPowerActive = false;
    }
}