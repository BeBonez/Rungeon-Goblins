using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject goblinEasterEgg;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("didTutorial", 0) == 0)
        {
            tutorialPanel.SetActive(true);
        }

        //AudioManager.Instance.PlayBGLoop(0);
    }

    void Start()
    {
        int number = Random.Range(0,101);

        if (number < 5) 
        {
            goblinEasterEgg.SetActive(true);
        }
    }
}
