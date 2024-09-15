using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Timer timer;
    [SerializeField] Animator animatior;
    

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Goblin":
                GetComponent<Animation>().Play("DefeatEnemy");
                Destroy(other.gameObject);
                timer.AddTime(3);
                break;
            case "Trap":
                GetComponent<Animation>().Play("TookDamage");
                timer.AddTime(-3);
                break;
            case "Hole":
                // chamar ap�s anima��o de cair no buraco
                timer.AddTime(-timer.GetMaxTime()); 
                break;
            case "Hourglass":
                Destroy(other.gameObject);
                timer.AddTime(timer.GetMaxTime());
                break;
            case "GoblinAttack":
                GetComponent<Animation>().Play("TookDamage");
                timer.AddTime(-3);
                break;
            case "Coin":
                Destroy(other.gameObject);
                gameManager.AddCoin(1);
                break;
        }
    }
}
