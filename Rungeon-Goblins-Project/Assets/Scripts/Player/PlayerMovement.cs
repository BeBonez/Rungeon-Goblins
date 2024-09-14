using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public void Move(Vector2 direction, int magnitude)
    {
        if (direction.y >= 1)
        {
            Debug.Log("UP");
        }
        else if (direction.y <= -1)
        {
            Debug.Log("DOWN");
        }
        else if (direction.x >= 1)
        {
            Debug.Log("RIGHT");
        }
        else if (direction.x <= -1)
        {
            Debug.Log("LEFT");
        }
    }
}
