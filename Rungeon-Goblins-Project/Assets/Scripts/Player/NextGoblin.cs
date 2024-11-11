using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextGoblin : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float playerOffset;
    public PaulScript pm;
    public GameObject player;

    void Update()
    {
        if (pm.IsFliyng() == true)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            ResetPos();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.tag);

        if (other.CompareTag("Goblin"))
        {
            Debug.Log("Yep");

            pm.nextGoblin = other.gameObject;

            ResetPos();
        }
    }

    private void ResetPos()
    {
        transform.position = new Vector3(player.transform.position.x, 8, player.transform.position.z + playerOffset);
    }
}
