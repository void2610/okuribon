using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class EnemyBase : MonoBehaviour
{
    public class AttackData
    {
        public string name;
        public Action<Player> action;
        public float probability;
        public Color color = Color.white;
        public string description;
    }
    public string enemyName = "Enemy";
    public int health = 100;
    public int maxHealth = 100;
    public int attack = 2;
    public int defense = 1;
    public int gold = 0;
    public int exp = 0;

    [SerializeField]
    private GameObject canvas;

    protected List<AttackData> enemyActions = new List<AttackData>();
    private AttackData nextAction;

    private TextMeshProUGUI nameText => canvas.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
    private TextMeshProUGUI healthText => canvas.transform.Find("HPText").GetComponent<TextMeshProUGUI>();
    private Slider healthSlider => canvas.transform.Find("HPSlider").GetComponent<Slider>();
    private TextMeshProUGUI attackText => canvas.transform.Find("AttackText").GetComponent<TextMeshProUGUI>();
    private Image attackImage => canvas.transform.Find("AttackIcon").GetComponent<Image>();
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health;
        healthText.text = health + "/" + maxHealth;
        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        healthSlider.value = health;
        healthText.text = health + "/" + maxHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Attack(Player player)
    {
        nextAction.action(player);
        DecideNextAction();
    }

    private void DecideNextAction()
    {
        float sum = 0;
        foreach (var a in enemyActions)
        {
            sum += a.probability;
        }
        float random = GameManager.instance.RandomRange(0.0f, sum);
        foreach (var a in enemyActions)
        {
            random -= a.probability;
            if (random <= 0)
            {
                nextAction = a;
                break;
            }
        }

        attackText.text = nextAction.name;
        attackImage.color = nextAction.color;
        attackImage.GetComponent<OverRayWindow>().text = nextAction.description;
    }

    protected virtual void NormalAttack(Player player)
    {
        if (player.isSaving) player.AddSaveFromEnemy(attack);
        else player.TakeDamage(attack);
    }

    protected virtual void PiercingAttack(Player player)
    {
        player.TakeDamage(attack);
    }

    public void Death()
    {
        this.transform.parent.parent.GetComponent<EnemyContainer>().RemoveEnemy(this.gameObject);
    }

    protected virtual void Awake()
    {
        nameText.text = enemyName;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        healthText.text = health + "/" + maxHealth;

        enemyActions.Add(new AttackData { name = "こうげき", action = NormalAttack, probability = 0.8f, color = Color.red, description = "いたいぞ！" });
        enemyActions.Add(new AttackData { name = "かんつう\nこうげき", action = PiercingAttack, probability = 0.2f, color = Color.yellow, description = "ためることができない" });

        DecideNextAction();
    }

    protected virtual void Update()
    {
    }
}
