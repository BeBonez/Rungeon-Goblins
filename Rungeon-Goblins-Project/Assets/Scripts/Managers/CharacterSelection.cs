using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class CharacterSelection : MonoBehaviour
{
    public int character;
    public static CharacterSelection Instance;
    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    
}
