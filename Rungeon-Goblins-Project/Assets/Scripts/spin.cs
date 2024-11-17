using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    [SerializeField] private float speed;
    private void FixedUpdate()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
