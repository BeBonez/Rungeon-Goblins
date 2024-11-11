using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaulPowerCast : MonoBehaviour
{
    [SerializeField] PlayerMovement pm;

    GameObject goblin;
    public bool CanTeleport = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goblin" && CanTeleport == true) {
            goblin = other.gameObject;
        }
    }

    public GameObject GetGoblin() {
        return goblin;
    }
}
