using UnityEngine;
using UnityEngine.InputSystem;
public class KeyBoard : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private InputAction touchAction;
    private Vector2 direction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchAction = playerInput.actions["KeyRead"];
    }

    private void OnEnable()
    {
        touchAction.started += GetDirection;
        touchAction.canceled += Move;
    }

    private void OnDisable()
    {
        touchAction.started -= GetDirection;
        touchAction.canceled -= Move;
    }

    private void GetDirection(InputAction.CallbackContext context)
    {
        direction = touchAction.ReadValue<Vector2>();
    }
    private void Move(InputAction.CallbackContext context)
    {
        playerMovement.Move(direction, 0);
    }
}
