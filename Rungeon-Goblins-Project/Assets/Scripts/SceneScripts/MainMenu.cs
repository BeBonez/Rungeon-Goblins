using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("didTutorial", 0) == 0)
        {
            tutorialPanel.SetActive(true);
        }

        //AudioManager.Instance.PlayBGLoop(0);
    }
}
