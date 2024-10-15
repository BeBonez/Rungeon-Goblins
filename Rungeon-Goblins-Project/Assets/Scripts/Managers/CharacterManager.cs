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
        selectedChar = characters[CharacterSelection.Instance.character];

        if (SceneManager.GetActiveScene().buildIndex != 0) {
        Debug.Log(selectedChar.name);
        Instantiate(selectedChar, spawnPoint, Quaternion.identity);
            uiCharactersPanel[CharacterSelection.Instance.character].SetActive(true);
        }

    }

    public void OnEnterMenu()
    {
        selectedChar = characters[CharacterSelection.Instance.character]; // Pega o numero do personagem selecionado e armazena
        charactersButton[CharacterSelection.Instance.character].Select(); // Deixa o botão selecionado
    }

    public void SelectCharacter(int index) {
        CharacterSelection.Instance.character = index;
        selectedChar = characters[CharacterSelection.Instance.character];
        // if (index == 0) {
        //     charactersUI[0].GetComponent<Animator>().SetTrigger("Idle");
        //     charactersUI[1].GetComponent<Animator>().SetTrigger("Spin");
        // }
        // else {
        //     charactersUI[1].GetComponent<Animator>().SetTrigger("Idle");
        //     charactersUI[0].GetComponent<Animator>().SetTrigger("Spin");
        // }
    }
}
