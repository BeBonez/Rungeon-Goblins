using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coins;
    [SerializeField] private TMP_Text personalBest;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            personalBest.text = PlayerPrefs.GetInt("PersonalBest", 0) + "m";
            coins.text = "$" + PlayerPrefs.GetInt("Money", 0);
        }
    }

    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
        Time.timeScale = 1.0f;
        switch(id)
        {
            case 0:
                AudioManager.Instance.PlayBGLoop(0);
                break;

            case 1:
                AudioManager.Instance.PlayBGLoop(1);
                break;
        }
    }

    public void ActivateElement(GameObject element)
    {
        AudioManager.Instance.PlaySFX(0);
        element.SetActive(true);
    }

    public void DeactivateElement(GameObject element)
    {
        AudioManager.Instance.PlaySFX(0);
        element.SetActive(false);
    }
}
