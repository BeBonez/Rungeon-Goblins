using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float smoothness;
    private float offSet;
    private Vector3 currentVelocity = Vector3.zero;
    private float originalX, originalY;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        offSet = transform.position.z - playerTransform.position.z;
        originalX = transform.position.x;
        originalY = transform.position.y;
    }
    private void FixedUpdate()
    {
        Vector3 playerPosition = new Vector3(originalX, originalY, playerTransform.position.z + offSet);
        transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref currentVelocity, smoothness);
    }
}
