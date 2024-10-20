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
                EnemyDefeat(other);
                timer.AddTime(3);
                break;
            case "Trap":
                TookDamage(3);
                break;
            case "Hole":
                // chamar após animação de cair no buraco
                timer.AddTime(-timer.GetMaxTime());
                break;
            case "Hourglass":
                timeAnimator.SetTrigger("AddTime");
                Destroy(other.gameObject);
                AudioManager.Instance.PlaySFX(3);
                timer.AddTime(timer.GetMaxTime());
                break;
            case "GoblinAttack":
                TookDamage(3);
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
        if (playerBase.CanTakeDamage())
        {
            timeAnimator.SetTrigger("TookDamage");
            cameraAnimator.SetTrigger("Default");
            cameraAnimator.SetTrigger("TookDamage");
            timer.AddTime(-value);
        }
    }

    private void EnemyDefeat(Collider other)
    {
        Destroy(other.gameObject);
        AudioManager.Instance.PlaySFX(1);
        cameraAnimator.SetTrigger("Default");
        cameraAnimator.SetTrigger("EnemyDefeat");
        timeAnimator.SetTrigger("AddTime");
    }
}
