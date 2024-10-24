using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayBGLoop(1);
    }
    private void OnEnable()
    {
        AudioManager.Instance.PlayBGLoop(0);
    }
}
