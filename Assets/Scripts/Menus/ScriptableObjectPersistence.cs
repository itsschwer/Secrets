﻿using UnityEngine;

[RequireComponent(typeof(SimpleSave))]
public class ScriptableObjectPersistence : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private VectorValue _playerPosition = default;
    public VectorValue playerPosition => _playerPosition;
    [SerializeField] private FloatMeter _health = default;
    public FloatMeter health => _health;
    [SerializeField] private FloatMeter _mana = default;
    public FloatMeter mana => _mana;
    [SerializeField] private Inventory _playerInventory = default;
    public Inventory playerInventory => _playerInventory;
    [SerializeField] private InventoryItem[] _inventoryItems = default;
    public InventoryItem[] inventoryItems => _inventoryItems;

    [SerializeField] private BoolValue[] _chests = default;
    public BoolValue[] chests => _chests;
    [SerializeField] private BoolValue[] _doors = default;
    public BoolValue[] doors => _doors;

    [Header("UI Updating")]
    public Signals coinSignal;

    public void ResetScriptableObjects()
    {
        ResetPlayer();
        ResetInventory();
        ResetBools();

        Debug.Log("Reset scriptable object save data.");
    }

    public void ResetPlayer()
    {
        health.max = 10;
        health.current = health.max;
        mana.max = 100;
        mana.current = mana.max;
    }

    public void ResetBools()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].RuntimeValue = chests[i].initialValue;
        }

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].RuntimeValue = doors[i].initialValue;
        }
    }

    public void ResetInventory()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i].numberHeld = 0;
        }

        playerInventory.coins = 0;
        coinSignal.Raise();

        playerInventory.contents = new System.Collections.Generic.List<InventoryItem>();

        playerInventory.currentItem = null;
        playerInventory.currentWeapon = null;
        playerInventory.currentArmor = null;
        playerInventory.currentHelmet = null;
        playerInventory.currentGloves = null;
        playerInventory.currentLegs = null;
        playerInventory.currentShield = null;
        playerInventory.currentRing = null;
        playerInventory.currentBow = null;
        playerInventory.currentSpellbook = null;
        playerInventory.currentAmulet = null;
        playerInventory.currentBoots = null;

        playerInventory.totalDefense = 0;
        playerInventory.totalCritChance = 0;
        playerInventory.totalMaxSpellDamage = 0;
        playerInventory.totalMinSpellDamage = 0;
    }
}