using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    GameObject player;
    [SerializeField] string type;
    [SerializeField] GameObject[] attacks;
    [SerializeField] private int downTime, upTime;
    [SerializeField] Animator animator;
    //private bool onOff;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        DeleteOutsideAttacks();
    }

    // IEnumerator Attack()
    // {
    //     // Deixar avisos ativos
    //     for (int i = 0; i < attacks.Length; i++) {
    //         attacks[i].SetActive(true);
    //     }
    //     yield return new WaitForSeconds(upTime);
    //     // Deixar avisos inativos
    //     for (int i = 0; i < attacks.Length; i++)
    //     {
    //         attacks[i].SetActive(false);
    //     }
    //     yield return new WaitForSeconds(downTime);
    //     onOff = false;
    // }

    private void FixedUpdate()
    {

        if (transform.position.z < player.transform.position.z - 45)
        {
            //StopCoroutine("Attack");
            Destroy(gameObject);
        }
    }

    public void DeleteOutsideAttacks() 
    {
        for (int i= 0 ; i < attacks.Length; i++) 
        {
            if (attacks[i].transform.position.x >= 11 || attacks[i].transform.position.x <= -11)
            {
                Destroy(attacks[i]);
            }
        }
    }
    public void PlayAnim(string direction) 
    {
        switch (type)
        {
            case "Club":
                if (direction == "Left")
                {
                    animator.Play("AttackLeft");
                }
                else if (direction == "Right")
                {
                    animator.Play("AttackRight");
                }
            break;

            default:
                animator.Play("Attack");
            break;
        }
    }
}
