﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    [Header("Door Attributes")]
    [SerializeField] private SpriteRenderer doorSpriteRenderer = default;
    [SerializeField] private Sprite closedSprite = default;
    [SerializeField] private Sprite openedSprite = default;
    [SerializeField] private BoxCollider2D doorCollider = default;
    [SerializeField] private Enemy[] enemies = default;

    [Header("Saving")]
    [SerializeField] private bool isDefeated;
    [SerializeField] private BoolValue storeDefeated;


    private void Awake()
    {
        if(storeDefeated)
        isDefeated = storeDefeated.RuntimeValue;
        {
            if (isDefeated)
            {
                OpenEnemyDoor();
            }
        }
    }

    private void OnEnable()
    {
        this.doorSpriteRenderer.sprite = closedSprite;
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].OnEnemyDied += CheckEnemies;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].OnEnemyDied -= CheckEnemies;
        }
    }

    public void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy)
            {
                return;
            }
        }
        OpenEnemyDoor();
    }


    public void OpenEnemyDoor()
    {
        if(openedSprite)
        {
            this.doorSpriteRenderer.sprite = openedSprite;
        }
        else
        {
            this.doorSpriteRenderer.enabled =false;
        }

        this.doorCollider.enabled = false;
    }
}
