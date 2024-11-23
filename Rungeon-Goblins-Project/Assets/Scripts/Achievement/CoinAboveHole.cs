using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAboveHole : MonoBehaviour
{
    public bool isAboveHole;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hole"))
        {
            isAboveHole = true;
        }
    }
}
