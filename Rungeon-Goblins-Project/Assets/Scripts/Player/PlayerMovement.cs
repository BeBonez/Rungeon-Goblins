using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float posX, posY, posZ;
    [SerializeField] protected float moveDistance;
    [SerializeField] public GameManager gameManager;
    [SerializeField] protected bool canTakeDamge = true;
    protected Vector3 lastPosition;
    protected bool canMove = true;

    public abstract void Move(Vector2 direction);
    //{
    //    //if (Time.timeScale > 0)
    //    //{
    //    //    if (direction.y >= 1)
    //    //    {
    //    //        //Debug.Log("UP");
    //    //        transform.position = new Vector3(posX, posY, posZ + moveDistance);
    //    //        canMoveBackwards = true; // PH
    //    //        gameManager.AddDistance(1);
    //    //    }
    //    //    else if (direction.y <= -1)
    //    //    {
    //    //        if (gameManager.GetDistance() != 0 && canMoveBackwards)
    //    //        {
    //    //            transform.position = new Vector3(posX, posY, posZ - moveDistance);
    //    //            canMoveBackwards = false;
    //    //            gameManager.AddDistance(-1);
    //    //        }
    //    //        //Debug.Log("DOWN");
    //    //    }
    //    //    else if (direction.x >= 1)
    //    //    {
    //    //        //Debug.Log("RIGHT");
    //    //        if (transform.position.x >= moveDistance)
    //    //        {
    //    //            transform.position = new Vector3(posX - moveDistance * 2, posY, posZ + moveDistance);
    //    //        }
    //    //        else
    //    //        {
    //    //            transform.position = new Vector3(posX + moveDistance, posY, posZ + moveDistance);
    //    //        }
    //    //        canMoveBackwards = true; // PH
    //    //        gameManager.AddDistance(1);

    //    //    }
    //    //    else if (direction.x <= -1)
    //    //    {
    //    //        //Debug.Log("LEFT");
    //    //        if (transform.position.x <= -moveDistance)
    //    //        {
    //    //            transform.position = new Vector3(posX + moveDistance * 2, posY, posZ + moveDistance);
    //    //        }
    //    //        else
    //    //        {
    //    //            transform.position = new Vector3(posX - moveDistance, posY, posZ + moveDistance);
    //    //        }
    //    //        canMoveBackwards = true; // PH
    //    //        gameManager.AddDistance(1);
    //    //    }
    //    //    UpdatePosition();
    //    }
    //}

    public void UpdatePosition()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
    }

    public bool CanTakeDamage()
    {
        return canTakeDamge;
    }

    public Vector3 GetLastPosition()
    {
        return lastPosition;
    }

    public void SwitchCanMove()
    {
        canMove = !canMove;
    }
}
