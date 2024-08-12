using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public int attack = 2;
    public int defense = 1;
    public int gold = 0;
    public int exp = 0;
    public int level = 1;
    public int maxSave = 50;
    public int save = 0;

    public bool isSaving = false;

    public void TakeDamage(int damage)
    {
        if (isSaving)
        {
            save += damage;
        }
        else
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
            }
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void AddExp(int amount)
    {
        exp += amount;
    }

    public void Attack(List<EnemyBase> enemies)
    {
        int a = save * attack;
        foreach (EnemyBase enemy in enemies)
        {
            enemy.TakeDamage(a);
        }
    }


    void Start()
    {
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
    }
}
