﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenseValueTextManager : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI defenseDisplay;

    private void Start()
    {
    //    UpdateDefenseValue();
    }

    private void update()
    {
      //  UpdateDefenseValue();
    }

    public void UpdateDefenseValue()
    {
        if (playerInventory.totalDefense > 0)
        {
            defenseDisplay.text = "" + playerInventory.totalDefense;
        }
        else
        {
            defenseDisplay.text = "0";
        }
    }

    
    private void OnEnable()
    {
        UpdateDefenseValue();
    }
    
}

