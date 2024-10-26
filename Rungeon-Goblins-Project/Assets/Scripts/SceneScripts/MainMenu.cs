using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.PlayBGLoop(0);
    }
}
