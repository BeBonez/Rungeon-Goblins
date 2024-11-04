using UnityEngine;

public class Worg : MonoBehaviour
{
    [SerializeField] float speed;
    private Animator animator;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed);

        if (transform.position.z < playerTransform.position.z - 40)
        {
            Destroy(gameObject);
        }
    }
}
