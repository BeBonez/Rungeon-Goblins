using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private GameManager gameManager;
    private Timer timer;
    private Animator timeAnimator;
    [SerializeField] private PlayerMovement playerBase;
    [SerializeField] GameObject explosionFX;
    Animator cameraAnimator;

    private void Start()
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timeAnimator = GameObject.Find("Time").GetComponent<Animator>();
        playerBase = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (timer.IsDead() == false)
        {
            switch (other.tag)
            {
                case "Goblin":
                    if (playerBase.IsPowerActive() == true && playerBase.GetName() == "Garry") { }
                    else
                    {
                        playerBase.GetAnimator().Rebind();
                        playerBase.GetAnimator().Play("Attack");
                    }

                    EnemyDefeat(other);
                    timer.AddTime(4.5f);
                    break;
                case "Trap":
                    if (playerBase.GetImortallity() == false)
                    {
                        if (playerBase.IsPowerActive() == false)
                        {
                            TookDamage(2.5f);
                        }
                    }

                    break;
                case "Hole":
                    if (playerBase.GetImortallity() == false)
                    {
                        if (playerBase.IsPowerActive() == false)
                        {
                            timer.DeadByHole(other.gameObject);
                            timer.AddTime(-timer.GetMaxTime());
                            //playerBase.DashOFF(false);
                        }
                    }

                    break;
                case "Hourglass":
                    timeAnimator.SetTrigger("AddTime");
                    Destroy(other.gameObject);
                    AudioManager.Instance.PlaySFX(3);
                    timer.AddTime(timer.GetMaxTime());
                    break;
                case "GoblinAttack":
                    if (playerBase.GetImortallity() == false)
                    {
                        if (playerBase.IsPowerActive() == false)
                        {
                            TookDamage(3.5f);
                        }
                    }

                    break;
                case "Coin":
                    if (playerBase.coinAboveHoleAchievement == false)
                    {
                        if (other.GetComponent<CoinAboveHole>().isAboveHole == true)
                        {
                            playerBase.coinAboveHoleAchievement = true;
                        }
                    }
                    Destroy(other.gameObject);
                    AudioManager.Instance.PlaySFX(2);
                    gameManager.AddCoin(1);
                    break;
            }
        }
    }

    private void TookDamage(float value)
    {
        int damageSound = Random.Range(4, 6);
        AudioManager.Instance.PlaySFX(damageSound);
        timeAnimator.SetTrigger("TookDamage");
        //cameraAnimator.SetTrigger("Default");
        cameraAnimator.SetTrigger("TookDamage");
        if (playerBase.GetInfTime() == false)
        {
            timer.AddTime(-value);
        }
    }

    private void EnemyDefeat(Collider other)
    {
        Vector3 fxPos = new Vector3(other.transform.position.x, other.transform.position.y + 5, other.transform.position.z);
        GameObject explosion = Instantiate(explosionFX, fxPos, Quaternion.identity);
        Destroy(explosion, 4f);
        Destroy(other.gameObject);

        cameraAnimator.SetTrigger("EnemyDefeat");

        playerBase.PlayHitSFX();

        if (playerBase.GetName() == "Paul")
        {
            playerBase.AddCharge(2, "Kill");

            playerBase.killed = true;

        }
        else
        {
            playerBase.AddCharge(1, "Kill");
        }
        playerBase.AddCharge(1, "Kill");
        playerBase.AddKill(1);
        //cameraAnimator.SetTrigger("Default");
        timeAnimator.SetTrigger("AddTime");
    }
}
