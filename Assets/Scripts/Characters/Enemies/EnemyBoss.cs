﻿using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;
using Pathfinding;

public class EnemyBoss : Character
{
    [Header("Enemy Stats")]
    [SerializeField] protected XPSystem levelSystem = default;
    [SerializeField] protected int enemyXp = default;
    [SerializeField] protected FloatValue maxHealth = default;
    [SerializeField] private float _health;
    public event Action OnEnemyTakeDamage;
    public event Action OnEnemyDied;
    public event Action OnMinionDied;
    [Header("BossFight-Values")]
    public bool isMinion = false;

    public override float health
    {
        get => _health;
        set
        {
            if (value > maxHealth.value)
            {
                value = maxHealth.value;
            }
            else if (value < 0)
            {
                value = 0;
            }

            if (value < _health)
            {
                OnEnemyTakeDamage?.Invoke();                                //Signal for when enemys take dmg (hopefully :) )                                                                            // Need to prevent the enemy from moving and set idle/moving Anim
            }

            _health = value;

            if (_health <= 0)
            {
                Die();
            }
        }
    }
    [Header("Enemy Attributes")]
    [SerializeField] protected string enemyName = default;      // Unused, is it necessary?
    public float moveSpeed = default;                           // Should make protected


    [Header("Death Effects")]
    [SerializeField] protected GameObject deathEffect = default;
    [SerializeField] private float deathEffectDelay = 1;

    [Header("Death Signal")]
    [SerializeField] protected Signals roomSignal = default;
    [SerializeField] protected LootTable thisLoot = default;


    protected virtual void OnEnable()
    {
        health = maxHealth.value;
        currentState = State.idle;
    }

    protected override void Awake()
    {
        base.Awake();

        health = maxHealth.value;
    }


    protected virtual void Die()
    {
        DeathEffect();
        thisLoot?.GenerateLoot(transform.position);
        levelSystem.AddExperience(enemyXp);

        if (roomSignal != null)
        {
            roomSignal.Raise();
        }

        this.gameObject.SetActive(false);
    }

    protected void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    public float GetPercentHealth() => (health * 100) / maxHealth.value;

    public void KillEnemy() => health = 0;


    public void GetHealed(float healAmount)
    {
        if (this.health < this.maxHealth.value)
        {
            if (this.health + healAmount > this.maxHealth.value)
            {
                this.health = this.maxHealth.value;
            }
            this.health += healAmount;
            DamagePopUpManager.RequestDamagePopUp(healAmount, this.transform);
        }
    }

    public float GetMaxHealth()
    {
        return this.maxHealth.value;
    }

}