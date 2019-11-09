﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public int Health = 100;
    public int MaxHealth = 100;

    public bool IsDead { get { return Health <= 0; } }

    public void TakeDamage(int damagePoints)
    {
        Health -= damagePoints;
        if (Health <= 0)
        {
            Health = 0;
        }
    }

    public void Heal(int healPoints)
    {
        Health += healPoints;
        if (Health >= MaxHealth)
        {
            Health = MaxHealth;
        }
    }
}