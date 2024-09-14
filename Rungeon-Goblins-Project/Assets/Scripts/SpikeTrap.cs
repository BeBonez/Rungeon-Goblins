using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private int downTime, upTime;
    private bool onOff;

    IEnumerator Movement()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        yield return new WaitForSeconds(upTime);
        transform.position = new Vector3(transform.position.x, -5, transform.position.z);
        yield return new WaitForSeconds(downTime);
        onOff = false;
    }

    private void FixedUpdate()
    {
        if (onOff == false) 
        { 
            onOff = true;
            StartCoroutine("Movement");
        }

        if (transform.position.z < player.transform.position.z - 15)
        {
            StopCoroutine("Movement");
            Destroy(gameObject);
        }
    }
}
