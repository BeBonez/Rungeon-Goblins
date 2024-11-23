using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] string direction;
    [SerializeField] Goblin goblin;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            goblin.PlayAnim(direction);
        }
    }
}
