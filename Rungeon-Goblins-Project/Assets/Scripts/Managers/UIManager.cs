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
        if (id == 0)
        {
            AudioManager.Instance.PlayBGLoop(0);
        }
        else if (id == 1)
        {
            int randomMusic = Random.Range(1, 4);
            AudioManager.Instance.PlayBGLoop(randomMusic);
        }
        Time.timeScale = 1f;
    }

    public void ActivateElement(GameObject element)
    {
        PlaySound(0);
        element.SetActive(true);
    }

    public void DeactivateElement(GameObject element)
    {
        PlaySound(0);
        element.SetActive(false);
    }
    
    public void PlaySound(int index)
    {
        AudioManager.Instance.PlaySFX(index);
    }
}
