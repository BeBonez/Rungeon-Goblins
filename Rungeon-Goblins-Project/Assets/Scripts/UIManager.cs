using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void ActivateElement(GameObject element)
    {
        element.SetActive(true);
    }

    public void DeactivateElement(GameObject element)
    {
        element.SetActive(false);
    }
}
