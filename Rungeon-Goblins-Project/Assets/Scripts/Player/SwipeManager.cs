using UnityEngine;
using UnityEngine.InputSystem;
public class SwipeManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction touchAction;
    private InputAction touchPosition;
    private Vector2 initialPosition;
    private Vector2 finalPosition;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchAction = playerInput.actions["TouchPress"];
    }


}
