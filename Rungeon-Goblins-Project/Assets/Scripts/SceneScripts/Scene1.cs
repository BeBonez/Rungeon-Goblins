using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1 : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayBGLoop(1);
    }
    private void OnEnable()
    {
        AudioManager.Instance.PlayBGLoop(1);
    }
}
