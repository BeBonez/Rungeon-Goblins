using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private int downTime, upTime;
    private bool onOff;
    private int sceneIndex;
    private float originalY;

    private void Start()
    {
        originalY = transform.position.y;
        player = GameObject.FindWithTag("Player");
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator Movement()
    {
        transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
        yield return new WaitForSeconds(upTime);
        transform.position = new Vector3(transform.position.x, originalY - 5, transform.position.z);
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

        if (sceneIndex != 0)
        {
            if (transform.position.z < player.transform.position.z - 15)
            {
                StopCoroutine("Movement");
                Destroy(gameObject);
            }
        }

    }
}
