using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Timer timer;
    [SerializeField] Animation shake;
    

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Goblin":
                shake.Play("DefeatEnemy");
                Destroy(other.gameObject);
                timer.AddTime(3);
                break;
            case "Trap":
                timer.AddTime(-3);
                break;
            case "Hole":
                // chamar após animação de cair no buraco
                timer.AddTime(-timer.GetMaxTime()); 
                break;
            case "Hourglass":
                Destroy(other.gameObject);
                timer.AddTime(timer.GetMaxTime());
                break;
            case "GoblinAttack":
                timer.AddTime(-3);
                break;
            case "Coin":
                Destroy(other.gameObject);
                gameManager.AddCoin(1);
                break;
        }
    }
}
