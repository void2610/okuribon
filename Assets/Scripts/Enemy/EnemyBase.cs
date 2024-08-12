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

    private List<AttackData> enemyActions = new List<AttackData>();
    private AttackData nextAction;

    private TextMeshProUGUI nameText => transform.Find("Canvas").transform.Find("NameText").GetComponent<TextMeshProUGUI>();
    private TextMeshProUGUI healthText => transform.Find("Canvas").transform.Find("HPText").GetComponent<TextMeshProUGUI>();
    private Slider healthSlider => transform.Find("Canvas").transform.Find("HPSlider").GetComponent<Slider>();
    private TextMeshProUGUI attackText => transform.Find("Canvas").transform.Find("AttackText").GetComponent<TextMeshProUGUI>();
    private Image attackImage => transform.Find("Canvas").transform.Find("AttackIcon").GetComponent<Image>();
    private CanvasGroup description => transform.Find("Canvas").transform.Find("Description").GetComponent<CanvasGroup>();

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
        description.GetComponentInChildren<TextMeshProUGUI>().text = nextAction.description;
    }

    protected virtual void NormalAttack(Player player)
    {
        if (player.isSaving) player.AddSave(attack);
        else player.TakeDamage(attack);
    }

    protected virtual void PiercingAttack(Player player)
    {
        player.TakeDamage(attack);
    }

    public void Death()
    {
        this.transform.parent.GetComponent<EnemyContainer>().RemoveEnemy(this.gameObject);
    }

    protected virtual void Awake()
    {
        nameText.text = enemyName;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        healthText.text = health + "/" + maxHealth;

        enemyActions.Add(new AttackData { name = "Normal Attack", action = NormalAttack, probability = 0.8f, color = Color.red, description = "hutuu" });
        enemyActions.Add(new AttackData { name = "Piercing Attack", action = PiercingAttack, probability = 0.2f, color = Color.yellow, description = "kantuu" });

        // マウスオーバー時に説明を表示
        EventTrigger trigger = attackImage.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { description.alpha = 1; });
        trigger.triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { description.alpha = 0; });
        trigger.triggers.Add(entry);

        DecideNextAction();
    }
}
