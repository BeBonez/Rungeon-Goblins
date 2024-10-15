using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private GameManager gameManager;
    private Timer timer;
    private Animator timeAnimator;
    Animator cameraAnimator;

    private void Start()
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timeAnimator = GameObject.Find("Time").GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Goblin":
                EnemyDefeat();
                Destroy(other.gameObject);
                timer.AddTime(3);
                break;
            case "Trap":
                TookDamage();
                timer.AddTime(-3);
                break;
            case "Hole":
                // chamar após animação de cair no buraco
                timer.AddTime(-timer.GetMaxTime()); 
                break;
            case "Hourglass":
                timeAnimator.SetTrigger("AddTime");
                Destroy(other.gameObject);
                timer.AddTime(timer.GetMaxTime());
                break;
            case "GoblinAttack":
                TookDamage();
                timer.AddTime(-3);
                break;
            case "Coin":
                Destroy(other.gameObject);
                gameManager.AddCoin(1);
                break;
        }
    }

    private void TookDamage()
    {
        timeAnimator.SetTrigger("TookDamage");
        cameraAnimator.SetTrigger("TookDamage");
    }

    private void EnemyDefeat()
    {
        cameraAnimator.SetTrigger("EnemyDefeat");
        timeAnimator.SetTrigger("AddTime");
    }
}
