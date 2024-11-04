using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] Transform point1, point2;
    Transform nextPoint;
    [SerializeField] float speed, rotateSpeed;
    private int rotateDirection = 1;
    private float x, y, z;
    private void Start()
    {
        nextPoint = point1;
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, nextPoint.position) < 0.0001f)
        {
            if (nextPoint.position == point1.position)
            {
                nextPoint = point2;
                rotateDirection *= -1;
            }
            else if (nextPoint.position == point2.position)
            {
                nextPoint = point1;
                rotateDirection *= -1;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPoint.position, speed * Time.deltaTime);

        transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime * rotateDirection);
    }
}
