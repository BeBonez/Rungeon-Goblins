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

        switch (other.tag)
        {

            case "Goblin":
                if (playerBase.IsFliyng() == false)
                {
                    if (playerBase.CanTakeDamage() == true)
                    {
                        playerBase.GetAnimator().Play("Attack");
                    }
                }
                EnemyDefeat(other);
                timer.AddTime(3);
                break;
            case "Trap":
                if (playerBase.IsFliyng() == false)
                {
                    TookDamage(3);
                }
                break;
            case "Hole":
                if (playerBase.IsFliyng() == false)
                {
                    playerBase.DashOFF(true);
                    timer.DeadByRole(other.gameObject);
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
                    TookDamage(3);
                }
                else if (playerBase.GetName() == "Paul" && playerBase.CanTakeDamage() == false)
                {
                    playerBase.GetAnimator().Play("Shield");
                }
                break;
            case "Coin":
                Destroy(other.gameObject);
                AudioManager.Instance.PlaySFX(2);
                gameManager.AddCoin(1);
                break;
        }

    }

    private void TookDamage(float value)
    {
        timeAnimator.SetTrigger("TookDamage");
        //cameraAnimator.SetTrigger("Default");
        cameraAnimator.SetTrigger("TookDamage");
        timer.AddTime(-value);
    }

    private void EnemyDefeat(Collider other)
    {
        Destroy(other.gameObject);
        AudioManager.Instance.PlaySFX(1);
        //cameraAnimator.SetTrigger("Default");
        cameraAnimator.SetTrigger("EnemyDefeat");
        timeAnimator.SetTrigger("AddTime");
    }
}
