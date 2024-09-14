using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float posX, posY, posZ;
    [SerializeField]private float distance;
    [SerializeField] private float horizontalDistance; //Pode ser apagado assim que tiver um mapa com tilings certos
    private void Start()
    {
        UpdatePosition();
    }
    public void Move(Vector2 direction)
    {

        if (direction.y >= 1)
        {
            Debug.Log("UP");
            transform.position = new Vector3(posX, posY, posZ + distance); 
        }
        else if (direction.y <= -1)
        {
            Debug.Log("DOWN");
            transform.position = new Vector3(posX, posY, posZ - distance);
        }
        else if (direction.x >= 1)
        {
            Debug.Log("RIGHT");
            if (transform.position.x >= horizontalDistance)
            {
                transform.position = new Vector3(posX - horizontalDistance * 2, posY, posZ + distance);
            }
            else
            {
                transform.position = new Vector3(posX + horizontalDistance, posY, posZ + distance);
            }
            
        }
        else if (direction.x <= -1)
        {
            Debug.Log("LEFT");
            if (transform.position.x <= -horizontalDistance)
            {
                transform.position = new Vector3(posX + horizontalDistance * 2, posY, posZ + distance);
            }
            else
            {
                transform.position = new Vector3(posX - horizontalDistance, posY, posZ + distance);
            }
        }
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
    }
}
