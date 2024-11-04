using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCubeScript : MonoBehaviour
{
    [SerializeField] GameObject tutorialEndScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialEndScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
