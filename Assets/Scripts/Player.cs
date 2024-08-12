using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public List<int> levelUpExp = new List<int> { 10, 20, 40, 80, 160, 320, 640, 1280, 2560, 5120 };
    public int level = 1;
    public int maxSave = 50;
    public int save = 0;

    public bool isSaving = false;

    private TextMeshProUGUI healthText => transform.Find("Canvas").transform.Find("HPText").GetComponent<TextMeshProUGUI>();
    private Slider healthSlider => transform.Find("Canvas").transform.Find("HPSlider").GetComponent<Slider>();
    private TextMeshProUGUI saveText => transform.Find("Canvas").transform.Find("SaveText").GetComponent<TextMeshProUGUI>();
    private Slider saveSlider => transform.Find("Canvas").transform.Find("SaveSlider").GetComponent<Slider>();

    public void TakeDamage(int damage)
    {
        if (isSaving)
        {
            save += damage;
            saveSlider.value = save;
            saveText.text = save + "/" + maxSave;
        }
        else
        {
            health -= damage;
            healthSlider.value = health;
            healthText.text = health + "/" + maxHealth;
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
        GameManager.instance.uiManager.UpdateCoinText(gold);
    }

    public void AddExp(int amount)
    {
        exp += amount;
        if (exp >= levelUpExp[level - 1])
        {
            LevelUp();
        }
        GameManager.instance.uiManager.UpdateExpText(exp, levelUpExp[level - 1]);
    }

    private void LevelUp()
    {
        level++;
        exp -= levelUpExp[level - 1];
        Heal(10);
        if (exp >= levelUpExp[level - 1])
        {
            LevelUp();
        }
        GameManager.instance.uiManager.UpdateExpText(exp, levelUpExp[level - 1]);
        GameManager.instance.ChangeState(GameManager.GameState.LevelUp);
    }

    public void Attack(List<EnemyBase> enemies)
    {
        int a = save * attack;
        foreach (EnemyBase enemy in enemies)
        {
            enemy.TakeDamage(a);
        }
        save = 0;
        saveSlider.value = save;
        saveText.text = save + "/" + maxSave;
    }

    void Awake()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        healthText.text = health + "/" + maxHealth;
        saveSlider.maxValue = maxSave;
        saveSlider.value = save;
        saveText.text = save + "/" + maxSave;

        GameManager.instance.uiManager.UpdateCoinText(gold);
        GameManager.instance.uiManager.UpdateExpText(exp, levelUpExp[level - 1]);
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
