using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public void Move(Vector2 direction, int magnitude)
    {
        if (Vector2.Dot(Vector2.up, direction) > magnitude)
        {
            Console.WriteLine("UP");
        }
        else if (Vector2.Dot(Vector2.down, direction) > magnitude)
        {
            Console.WriteLine("DOWN");
        }
        else if (Vector2.Dot(Vector2.right, direction) > magnitude)
        {
            Console.WriteLine("RIGHT");
        }
        else if (Vector2.Dot(Vector2.left, direction) > magnitude)
        {
            Console.WriteLine("LEFT");
        }
    }
}
