using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] attacks;
    [SerializeField] private int downTime, upTime;
    private bool onOff;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    IEnumerator Attack()
    {
        // Deixar avisos ativos
        for (int i = 0; i < attacks.Length; i++) {
            attacks[i].SetActive(true);
        }
        yield return new WaitForSeconds(upTime);
        // Deixar avisos inativos
        for (int i = 0; i < attacks.Length; i++)
        {
            attacks[i].SetActive(false);
        }
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
