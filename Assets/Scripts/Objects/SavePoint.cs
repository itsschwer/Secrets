﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] private GameObject saveMenu;
    public GameObject firstButtonSave;
    [Header("Slot Values")]
    [SerializeField] private Button saveSlot1;
    [SerializeField] private Button saveSlot2;
    [SerializeField] private Button saveSlot3;
    [SerializeField] private StringValue saveSlot1Text;
    [SerializeField] private StringValue saveSlot2Text;
    [SerializeField] private StringValue saveSlot3Text;

    [Header("PlayerData")]
    [SerializeField] CharacterAppearance playerAppearance;
    [SerializeField] XPSystem playerXP;



    private void Awake()
    {
        saveMenu.SetActive(false);
        saveSlot1.GetComponentInChildren<Text>().text = saveSlot1Text.RuntimeValue;
        saveSlot2.GetComponentInChildren<Text>().text = saveSlot2Text.RuntimeValue;
        saveSlot3.GetComponentInChildren<Text>().text = saveSlot3Text.RuntimeValue;
    }

    void Update()
    {
        if(playerInRange == true && Input.GetButtonDown("Interact"))     // Create new button Interact instead of run
        {
            saveMenu.SetActive(true);
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonSave);
            // Save("SecretsSave");                 // <--- Instead of directly save you can open your canvas
            // Debug.Log("Game was saved!");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInRange = false;
        }
    }

    public void SaveSlot1()
    {
        Save("saveSlot1");
        saveSlot1.GetComponentInChildren<Text>().text = playerAppearance.playerName + " Level: " + playerXP.level + " " + SceneManager.GetActiveScene().name;
        saveSlot1Text.RuntimeValue = saveSlot1.GetComponentInChildren<Text>().text;
    }

    public void SaveSlot2()
    {
        Save("saveSlot2");
        saveSlot2.GetComponentInChildren<Text>().text = playerAppearance.playerName + " Level: " + playerXP.level + " " + SceneManager.GetActiveScene().name;
        saveSlot2Text.RuntimeValue = saveSlot2.GetComponentInChildren<Text>().text;
    }

    public void SaveSlot3()
    {
        Save("saveSlot3");
        saveSlot3.GetComponentInChildren<Text>().text = playerAppearance.playerName + " Level: " + playerXP.level + " " + SceneManager.GetActiveScene().name;
        saveSlot3Text.RuntimeValue = saveSlot3.GetComponentInChildren<Text>().text;
    }

    public void CancelSave()
    {
        saveMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Save(string saveSlot)
    {
        SimpleSave.Instance.Save(saveSlot);           //<---- This is how i save (use your own method)
        Time.timeScale = 1;
        saveMenu.SetActive(false);
        Debug.Log("Game was saved!");
    }


}
