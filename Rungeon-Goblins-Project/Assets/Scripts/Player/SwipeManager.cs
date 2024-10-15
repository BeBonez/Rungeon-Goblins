using UnityEngine;
using UnityEngine.InputSystem;
public class SwipeManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private ScriptableObject oi;
    private PlayerInput playerInput;
    private InputAction touchAction;
    private InputAction touchPosition;
    private Vector2 initialPosition;
    private Vector2 finalPosition;
    [SerializeField]private int magnitude = 5;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchAction = playerInput.actions["TouchPress"];
        touchPosition = playerInput.actions["TouchPosition"];
    }

    private void OnEnable()
    {
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
    }

    //Ao soltar. Pega posição final
    private void TouchEnded(InputAction.CallbackContext context)
    {
        finalPosition = touchPosition.ReadValue<Vector2>();
        GetDirection();
    }

    private void GetDirection()
    {
        Vector2 direction = finalPosition - initialPosition;

        if (direction.magnitude > magnitude)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                direction.y = 0;
            }
            else
            {
                direction.x = 0;
            }

            direction = direction.normalized;
            playerMovement.Move(direction);
        }
    }
}
