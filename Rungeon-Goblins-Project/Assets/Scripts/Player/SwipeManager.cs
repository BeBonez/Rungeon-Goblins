using UnityEngine;
using UnityEngine.InputSystem;
public class SwipeManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private InputAction touchAction;
    private InputAction touchPosition;
    private Vector2 initialPosition;
    private Vector2 finalPosition;
    private bool isSwiping;
    private int magnitude = 40;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchAction = playerInput.actions["TouchPress"];
        touchPosition = playerInput.actions["TouchPosition"];
    }

    private void OnEnable()
    {
        Debug.Log("Apertou");
        touchAction.performed += TouchStarted;
        touchAction.canceled += TouchEnded;
    }

    private void OnDisable()
    {
        touchAction.performed -= TouchStarted;
        touchAction.canceled -= TouchEnded;
    }

    //Toque inicial. Pega coordenada inicial
    private void TouchStarted(InputAction.CallbackContext context)
    {
        initialPosition = touchPosition.ReadValue<Vector2>();
        isSwiping = true;
    }

    //Ao soltar. Pega posição final
    private void TouchEnded(InputAction.CallbackContext context)
    {
        finalPosition = touchPosition.ReadValue<Vector2>();
        isSwiping = false;
        GetDirection();
    }

    private void GetDirection()
    {
        Vector2 direction = (finalPosition - initialPosition).normalized;

        if (direction.magnitude > magnitude)
        {
            playerMovement.Move(direction, magnitude);
        }
    }
}
