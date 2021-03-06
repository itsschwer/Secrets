﻿using System;
using UnityEngine;

public class InventoryDisplay : ItemDisplay
{
    [SerializeField] private Inventory _inventory;
    public override Inventory inventory {
        get => _inventory;
        protected set => _inventory = value;
    }

    [Header("Equipment Slots")]
    [SerializeField] private ItemSlot weaponSlot = default;
    [SerializeField] private ItemSlot armorSlot = default;
    [SerializeField] private ItemSlot helmetSlot = default;
    [SerializeField] private ItemSlot gloveSlot = default;
    [SerializeField] private ItemSlot legsSlot = default;
    [SerializeField] private ItemSlot shieldSlot = default;
    [SerializeField] private ItemSlot ringSlot = default;

    [SerializeField] private ItemSlot spellbookSlot = default;
    [SerializeField] private ItemSlot spellbookTwoSlot = default;
    [SerializeField] private ItemSlot spellbookThreeSlot = default;

    [SerializeField] private ItemSlot amuletSlot = default;
    [SerializeField] private ItemSlot bootsSlot = default;
    [SerializeField] private ItemSlot lampSlot = default;
    [SerializeField] private ItemSlot cloakSlot = default;
    [SerializeField] private ItemSlot beltSlot = default;
    [SerializeField] private ItemSlot shoulderSlot = default;
    [SerializeField] private ItemSlot sealSlot = default;

    [Header("Quest Slots")]
    [SerializeField] private ItemSlot seedSlot = default;
    [SerializeField] private ItemSlot runeSlot = default;
    [SerializeField] private ItemSlot gemSlot = default;
    [SerializeField] private ItemSlot pearlSlot = default;
    [SerializeField] private ItemSlot dragonEggSlot = default;
    [SerializeField] private ItemSlot artifactSlot = default;
    [SerializeField] private ItemSlot crownSlot = default;
    [SerializeField] private ItemSlot scepterSlot = default;

    public Action<Item> OnSlotSelected { get; set; }
    public Action<Item> OnSlotUsed { get; set; }

    private void OnEnable() {
        inventory.items.OnContentsChanged += UpdateItemSlots;
        inventory.OnEquipmentChanged += UpdateEquipmentSlots;
        UpdateItemSlots();
        UpdateEquipmentSlots();
    }

    private void OnDisable() {
        inventory.items.OnContentsChanged -= UpdateItemSlots;
        inventory.OnEquipmentChanged -= UpdateEquipmentSlots;
    }

    public void SubscribeToEquipmentSlotSelected(Action<Item> action)
    {
        weaponSlot.OnSlotSelected += action;
        armorSlot.OnSlotSelected += action;
        helmetSlot.OnSlotSelected += action;
        gloveSlot.OnSlotSelected += action;
        legsSlot.OnSlotSelected += action;
        shieldSlot.OnSlotSelected += action;
        ringSlot.OnSlotSelected += action;
        spellbookThreeSlot.OnSlotSelected += action;
        spellbookSlot.OnSlotSelected += action;
        spellbookTwoSlot.OnSlotSelected += action;
        amuletSlot.OnSlotSelected += action;
        bootsSlot.OnSlotSelected += action;
        lampSlot.OnSlotSelected += action;
        cloakSlot.OnSlotSelected += action;
        beltSlot.OnSlotSelected += action;
        shoulderSlot.OnSlotSelected += action;
        sealSlot.OnSlotSelected += action;
        seedSlot.OnSlotSelected += action;
        runeSlot.OnSlotSelected += action;
        gemSlot.OnSlotSelected += action;
        pearlSlot.OnSlotSelected += action;
        dragonEggSlot.OnSlotSelected += action;
        artifactSlot.OnSlotSelected += action;
        crownSlot.OnSlotSelected += action;
        scepterSlot.OnSlotSelected += action;
    }

    public void SubscribeToEquipmentSlotUsed(Action<Item> action)
    {
        weaponSlot.OnSlotUsed += action;
        armorSlot.OnSlotUsed += action;
        helmetSlot.OnSlotUsed += action;
        gloveSlot.OnSlotUsed += action;
        legsSlot.OnSlotUsed += action;
        shieldSlot.OnSlotUsed += action;
        ringSlot.OnSlotUsed += action;
        spellbookThreeSlot.OnSlotUsed += action;
        spellbookSlot.OnSlotUsed += action;
        spellbookTwoSlot.OnSlotUsed += action;
        amuletSlot.OnSlotUsed += action;
        bootsSlot.OnSlotUsed += action;
        lampSlot.OnSlotUsed += action;
        cloakSlot.OnSlotUsed += action;
        beltSlot.OnSlotUsed += action;
        shoulderSlot.OnSlotUsed += action;
        sealSlot.OnSlotUsed += action;
        seedSlot.OnSlotUsed += action;
        runeSlot.OnSlotUsed += action;
        gemSlot.OnSlotUsed += action;
        pearlSlot.OnSlotUsed += action;
        dragonEggSlot.OnSlotUsed += action;
        artifactSlot.OnSlotUsed += action;
        crownSlot.OnSlotUsed += action;
        scepterSlot.OnSlotUsed += action;    
    }

    public void UnsubscribeFromEquipmentSlotSelected(Action<Item> action)
    {
        weaponSlot.OnSlotSelected -= action;
        armorSlot.OnSlotSelected -= action;
        helmetSlot.OnSlotSelected -= action;
        gloveSlot.OnSlotSelected -= action;
        legsSlot.OnSlotSelected -= action;
        shieldSlot.OnSlotSelected -= action;
        ringSlot.OnSlotSelected -= action;
        spellbookThreeSlot.OnSlotSelected -= action;
        spellbookSlot.OnSlotSelected -= action;
        spellbookTwoSlot.OnSlotSelected -= action;
        amuletSlot.OnSlotSelected -= action;
        bootsSlot.OnSlotSelected -= action;
        lampSlot.OnSlotSelected -= action;
        cloakSlot.OnSlotSelected -= action;
        beltSlot.OnSlotSelected -= action;
        shoulderSlot.OnSlotSelected -= action;
        sealSlot.OnSlotSelected -= action;
        seedSlot.OnSlotSelected -= action;
        runeSlot.OnSlotSelected -= action;
        gemSlot.OnSlotSelected -= action;
        pearlSlot.OnSlotSelected -= action;
        dragonEggSlot.OnSlotSelected -= action;
        artifactSlot.OnSlotSelected -= action;
        crownSlot.OnSlotSelected -= action;
        scepterSlot.OnSlotSelected -= action;
    }

    public void UnsubscribeToEquipmentSlotUsed(Action<Item> action)
    {
        weaponSlot.OnSlotUsed -= action;
        armorSlot.OnSlotUsed -= action;
        helmetSlot.OnSlotUsed -= action;
        gloveSlot.OnSlotUsed -= action;
        legsSlot.OnSlotUsed -= action;
        shieldSlot.OnSlotUsed -= action;
        ringSlot.OnSlotUsed -= action;
        spellbookThreeSlot.OnSlotUsed -= action;
        spellbookSlot.OnSlotUsed -= action;
        spellbookTwoSlot.OnSlotUsed -= action;
        amuletSlot.OnSlotUsed -= action;
        bootsSlot.OnSlotUsed -= action;
        lampSlot.OnSlotUsed -= action;
        cloakSlot.OnSlotUsed -= action;
        beltSlot.OnSlotUsed -= action;
        shoulderSlot.OnSlotUsed -= action;
        sealSlot.OnSlotUsed -= action;
        seedSlot.OnSlotUsed -= action;
        runeSlot.OnSlotUsed -= action;
        gemSlot.OnSlotUsed -= action;
        pearlSlot.OnSlotUsed -= action;
        dragonEggSlot.OnSlotUsed -= action;
        artifactSlot.OnSlotUsed -= action;
        crownSlot.OnSlotUsed -= action;
        scepterSlot.OnSlotUsed -= action;
    }

    public void UpdateEquipmentSlots()
    {
        weaponSlot.SetItem(inventory.currentWeapon, 0);
        armorSlot.SetItem(inventory.currentArmor, 0);
        helmetSlot.SetItem(inventory.currentHelmet, 0);
        gloveSlot.SetItem(inventory.currentGloves, 0);
        legsSlot.SetItem(inventory.currentLegs, 0);
        shieldSlot.SetItem(inventory.currentShield, 0);
        ringSlot.SetItem(inventory.currentRing, 0);
        spellbookSlot.SetItem(inventory.currentSpellbook, 0);
        spellbookTwoSlot.SetItem(inventory.currentSpellbookTwo, 0);
        spellbookThreeSlot.SetItem(inventory.currentSpellbookThree, 0);
        amuletSlot.SetItem(inventory.currentAmulet, 0);
        bootsSlot.SetItem(inventory.currentBoots, 0);
        lampSlot.SetItem(inventory.currentLamp, 0);
        cloakSlot.SetItem(inventory.currentCloak, 0);
        beltSlot.SetItem(inventory.currentBelt, 0);
        shoulderSlot.SetItem(inventory.currentShoulder, 0);
        sealSlot.SetItem(inventory.currentSeal, 0);
        seedSlot.SetItem(inventory.currentSeed, 0);
        runeSlot.SetItem(inventory.currentRune, 0);
        gemSlot.SetItem(inventory.currentGem, 0);
        pearlSlot.SetItem(inventory.currentPearl, 0);
        dragonEggSlot.SetItem(inventory.currentDragonEgg, 0);
        artifactSlot.SetItem(inventory.currentArtifact, 0);
        crownSlot.SetItem(inventory.currentCrown, 0);
        scepterSlot.SetItem(inventory.currentScepter, 0);
    }

    protected override void InstantiateSlots()
    {
        for (int i = slots.Count; i < inventory.items.Count; i++)
        {
            var newSlot = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, itemSlotParent.transform).GetComponent<ItemSlot>();
            slots.Add(newSlot);

            if (OnSlotSelected != null)
            {
                newSlot.OnSlotSelected += OnSlotSelected;
            }
            
            if (OnSlotUsed != null)
            {
                newSlot.OnSlotUsed += OnSlotUsed;
            }
        }
    }
}
