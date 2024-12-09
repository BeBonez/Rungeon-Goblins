using UnityEngine;

public class Worg : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private Animator animator;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0f))
        {
            if (hit.collider.gameObject.tag == "Trap" || hit.collider.gameObject.tag == "Hole")
            {
                animator.Play("Jump");
            }
        }

        if (transform.position.z < playerTransform.position.z - 40)
        {
            Destroy(gameObject);
        }
    }
}
