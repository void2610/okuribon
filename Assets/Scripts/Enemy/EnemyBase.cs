using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
        if (health <= 1)
        {
            health = 1;
        }
        healthSlider.value = health;
        healthText.text = health + "/" + maxHealth;
    }

    public void TakeDamageFromReturn(int damage)
    {
        health -= damage;
        healthSlider.value = health;
        healthText.text = health + "/" + maxHealth;
        if (health <= 0)
        {
            health = 0;
            healthText.text = health + "/" + maxHealth;
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

    public void OnAppear()
    {
        CanvasGroup cg = canvas.GetComponent<CanvasGroup>();
        cg.DOFade(1, 0.5f);
        this.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
    }

    public void OnDisappear()
    {
        CanvasGroup cg = canvas.GetComponent<CanvasGroup>();
        cg.DOFade(0, 0.5f);
        // ゆらゆらと左右に揺れながら上に登っていく
        this.transform.DOPunchPosition(new Vector3(0.5f, 0, 0), 2f, 3, 1f).SetRelative(true);
        this.transform.DOMoveY(1, 2f).SetRelative(true);
        this.GetComponent<SpriteRenderer>().DOFade(0, 2f).OnComplete(() =>
        {
            Destroy(this.transform.parent.gameObject);
        });
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

    public void Death()
    {
        this.transform.parent.parent.GetComponent<EnemyContainer>().RemoveEnemy(this.gameObject);
    }

    protected virtual void Awake()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        canvas.GetComponent<CanvasGroup>().alpha = 0;
        nameText.text = enemyName;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        healthText.text = health + "/" + maxHealth;

        enemyActions.Add(new AttackData { name = "こうげき", action = NormalAttack, probability = 0.8f, color = Color.red, description = "いたい！" });

        DecideNextAction();
        OnAppear();
    }

    protected virtual void Update()
    {
    }
}
