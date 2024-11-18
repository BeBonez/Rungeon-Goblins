using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    bool stepped;
    [SerializeField] float timeToFall;
    [SerializeField] GameObject cover, hole;
    float actualTime;

    void Update()
    {
        if (stepped == true)
        {
            actualTime += Time.deltaTime;

            if (actualTime >= timeToFall)
            {
                cover.GetComponent<Animator>().enabled = true;

                hole.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stepped = true;
        }
    }
}
