using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel, languagePanel;
    [SerializeField] GameObject goblinEasterEgg;
    [SerializeField] LocalizationManager localizationManager;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("choseLanguage", 0) == 0)
        {
            languagePanel.SetActive(true);
        }
        else
        {
            int language = PlayerPrefs.GetInt("language", 0);

            localizationManager.ChangeLocale(language);
        }

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
