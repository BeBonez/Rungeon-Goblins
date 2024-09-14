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
        Debug.Log("Soltou");
        finalPosition = touchPosition.ReadValue<Vector2>();
        GetDirection();
    }

    private void GetDirection()
    {
        Debug.Log("Soltou Legal");
        Vector2 direction = finalPosition - initialPosition;

        Debug.Log("Direcao: " + direction.magnitude + " |||||| " + magnitude + " |||||||||| " + direction);
        if (direction.magnitude > magnitude)
        {
            Debug.Log("Soltou Mesmo");
            playerMovement.Move(direction, magnitude);
        }
    }
}
