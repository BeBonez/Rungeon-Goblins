using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{

    [SerializeField] private Button[] charactersButton; 
    [SerializeField] private GameObject selectedChar;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] characters;
    [SerializeField] private GameObject[] charactersUI;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private GameObject[] uiCharactersPanel;
    [SerializeField] private GameObject[] selectionCirclesSprites;

    private void Awake() {
        selectedChar = characters[PlayerPrefs.GetInt("SelectedChar", 0)];

        if (SceneManager.GetActiveScene().buildIndex != 0) {
        Debug.Log(selectedChar.name);
        Instantiate(selectedChar, spawnPoint, Quaternion.identity);
            uiCharactersPanel[PlayerPrefs.GetInt("SelectedChar", 0)].SetActive(true);
        }

    }

    public void OnEnterMenu()
    {
        selectedChar = characters[PlayerPrefs.GetInt("SelectedChar", 0)]; // Pega o numero do personagem selecionado e armazena
        selectionCirclesSprites[PlayerPrefs.GetInt("SelectedChar", 0)].SetActive(true); // Deixa o botão selecionado
    }

    public void SelectCharacter(int index) {
        PlayerPrefs.SetInt("SelectedChar", index);

        int selectedCharNumber = PlayerPrefs.GetInt("SelectedChar", 0);

        selectedChar = characters[selectedCharNumber];

        if (selectedCharNumber == 0)
        {
            selectionCirclesSprites[1].SetActive(false);
            selectionCirclesSprites[0].SetActive(true);
        }
        else
        {
            selectionCirclesSprites[0].SetActive(false);
            selectionCirclesSprites[1].SetActive(true);  
        }
        

    }
}
