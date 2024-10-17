using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarryScript : PlayerMovement
{
    public override void Move(Vector2 direction)
    {
        if (canMove)
        {
            lastPosition = transform.position;

            throw new System.NotImplementedException();
        }

    }
}
