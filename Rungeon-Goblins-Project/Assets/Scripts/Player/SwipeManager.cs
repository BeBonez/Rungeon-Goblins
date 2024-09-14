using UnityEngine;
using UnityEngine.InputSystem;
public class KeyBoard : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private InputAction touchAction;
    private InputAction touchPosition;
    private Vector2 direction;
    private Vector2 finalPosition;
    private bool isSwiping;
    [SerializeField] private int magnitude = 5;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchAction = playerInput.actions["KeyRead"];
    }

    private void Update()
    {
        direction = touchAction.ReadValue<Vector2>();
        Debug.Log(direction);
    }

    private void FixedUpdate()
    {
        playerMovement.Move(direction, 0);
    }
}
