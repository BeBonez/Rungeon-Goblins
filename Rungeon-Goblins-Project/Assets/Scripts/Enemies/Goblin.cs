using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject LeftAttack;
    [SerializeField] GameObject RightAttack;
    [SerializeField] private int downTime, upTime;
    private bool onOff;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    IEnumerator Attack()
    {
        // Deixar avisos ativos
        LeftAttack.SetActive(true);
        RightAttack.SetActive(true);
        yield return new WaitForSeconds(upTime);
        // Deixar avisos inativos
        LeftAttack.SetActive(false);
        RightAttack.SetActive(false);
        yield return new WaitForSeconds(downTime);
        onOff = false;
    }

    private void FixedUpdate()
    {
        if (onOff == false)
        {
            onOff = true;
            StartCoroutine("Attack");
        }

        if (transform.position.z < player.transform.position.z - 15)
        {
            StopCoroutine("Attack");
            Destroy(gameObject);
        }
    }
}
