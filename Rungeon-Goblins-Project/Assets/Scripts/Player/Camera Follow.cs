using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private Transform playerTransform;
    [SerializeField]private float offSet, originalX, originalY;

    private void Awake()
    {
        offSet = transform.position.z - playerTransform.position.z;
        originalX = transform.position.x;
        originalY = transform.position.y;
    }

    private void LateUpdate()
    {
        Vector3 playerPosition = new Vector3(originalX, originalY, playerTransform.position.z + offSet);
        transform.position = playerPosition;
    }
}
