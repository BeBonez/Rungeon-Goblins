using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}
