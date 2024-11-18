using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private GameManager gameManager;
    private Timer timer;
    private Animator timeAnimator;
    [SerializeField] private PlayerMovement playerBase;
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
                    if (playerBase.IsFliyng() == false)
                    {
                        playerBase.GetAnimator().Play("Attack");
                    }
                    EnemyDefeat(other);
                    timer.AddTime(4.5f);
                    break;
                case "Trap":
                    if (playerBase.IsFliyng() == false)
                    {
                        TookDamage(2.5f);
                    }
                    break;
                case "Hole":
                    if (playerBase.IsFliyng() == false)
                    {
                        timer.DeadByHole(other.gameObject);
                        timer.AddTime(-timer.GetMaxTime());
                        //playerBase.DashOFF(false);
                    }
                    break;
                case "Hourglass":
                    timeAnimator.SetTrigger("AddTime");
                    Destroy(other.gameObject);
                    AudioManager.Instance.PlaySFX(3);
                    timer.AddTime(timer.GetMaxTime());
                    break;
                case "GoblinAttack":

                    if (playerBase.CanTakeDamage())
                    {
                        TookDamage(3.5f);
                    }
                    // else if (playerBase.GetName() == "Paul" && playerBase.CanTakeDamage() == false)
                    // {
                    //     playerBase.GetAnimator().Play("Shield");
                    // }
                    break;
                case "Coin":
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
        timer.AddTime(-value);
    }

    private void EnemyDefeat(Collider other)
    {
        Destroy(other.gameObject);
        playerBase.PlayHitSFX();

        if (playerBase.name == "Paul") {
            playerBase.AddCharge(2, "Kill");
        }
        else {
            playerBase.AddCharge(1, "Kill");
        }
        playerBase.AddCharge(1, "Kill");
        playerBase.AddKill(1);
        //cameraAnimator.SetTrigger("Default");
        cameraAnimator.SetTrigger("EnemyDefeat");
        timeAnimator.SetTrigger("AddTime");
    }
}
