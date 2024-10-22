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
        charactersButton[PlayerPrefs.GetInt("SelectedChar", 0)].Select(); // Deixa o botão selecionado
    }

    public void SelectCharacter(int index) {
        PlayerPrefs.SetInt("SelectedChar", index);
        selectedChar = characters[PlayerPrefs.GetInt("SelectedChar", 0)];

    }
}
