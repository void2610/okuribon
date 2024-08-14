using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public float attack = 1.5f;
    public int gold = 0;
    public int exp = 0;
    public List<int> levelUpExp = new List<int> { 10, 20, 40, 80, 160, 320, 640, 1280, 2560, 5120 };
    public int level = 1;
    public int maxSave = 5;
    public int save = 0;

    public bool isSaving { get; private set; } = false;

    [SerializeField]
    private GameObject canvas;

    private TextMeshProUGUI healthText => canvas.transform.Find("HPText").GetComponent<TextMeshProUGUI>();
    private Slider healthSlider => canvas.transform.Find("HPSlider").GetComponent<Slider>();
    private TextMeshProUGUI saveText => canvas.transform.Find("SaveText").GetComponent<TextMeshProUGUI>();
    private Slider saveSlider => canvas.transform.Find("SaveSlider").GetComponent<Slider>();

    public void UpdateStatusDisplay()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        healthText.text = health + "/" + maxHealth;
        saveSlider.maxValue = maxSave;
        saveSlider.value = save;
        saveText.text = save + "/" + maxSave;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
        }
        UpdateStatusDisplay();
    }

    public void AddSaveFromEnemy(int amount)
    {
        if (save + amount > maxSave)
        {
            TakeDamage(save + amount);
            save = 0;
        }
        else if (save + amount < 0)
        {
            save = 0;
        }
        else
        {
            save += amount;
        }
        UpdateStatusDisplay();
    }

    public void AddSaveFromItem(int amount)
    {
        save += amount;
        if (save > maxSave)
        {
            save = maxSave;
        }
        UpdateStatusDisplay();
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateStatusDisplay();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        if (gold < 0)
        {
            gold = 0;
        }
        GameManager.instance.uiManager.UpdateCoinText(gold);
    }

    public void AddExp(int amount)
    {
        exp += amount;
        GameManager.instance.uiManager.UpdateExpText(exp, levelUpExp[level - 1]);
    }

    private bool CheckAndLevelUp()
    {
        if (exp < levelUpExp[level - 1])
        {
            return false;
        }

        exp -= levelUpExp[level - 1];
        level++;
        Heal(5);
        SeManager.instance.PlaySe("levelUp");
        GameManager.instance.uiManager.UpdateExpText(exp, levelUpExp[level - 1]);
        GameManager.instance.uiManager.UpdateLevelText(level);
        GameManager.instance.uiManager.remainingLevelUps++;
        GameManager.instance.ChangeState(GameManager.GameState.LevelUp);

        if (exp >= levelUpExp[level - 1])
        {
            CheckAndLevelUp();
        }

        return true;
    }

    public void Attack(List<EnemyBase> enemies)
    {
        GameManager.instance.ChangeState(GameManager.GameState.PlayerAttack);
        SeManager.instance.PlaySe("enemyAttack");
        int a = Mathf.FloorToInt(attack * (float)save);
        foreach (EnemyBase enemy in enemies)
        {
            enemy.TakeDamage(a);
        }

        this.transform.DOMoveX(0.75f, 0.02f).SetRelative(true).OnComplete(() =>
        {
            this.transform.DOMoveX(-0.75f, 0.2f).SetRelative(true).SetEase(Ease.OutExpo);
            Utils.instance.WaitAndInvoke(1f, () =>
            {
                if (CheckAndLevelUp())
                {
                    GameManager.instance.ChangeState(GameManager.GameState.LevelUp);
                }
                else
                {
                    GameManager.instance.ChangeState(GameManager.GameState.EnemyAttack);
                }
                save = 0;
                UpdateStatusDisplay();
            });
        });
    }

    public void EnableSave(bool enable)
    {
        if (enable) Save();
        else isSaving = false;
    }

    private void Save()
    {
        GameManager.instance.ChangeState(GameManager.GameState.PlayerAttack);
        isSaving = true;
        GameManager.instance.ChangeState(GameManager.GameState.EnemyAttack);
    }

    public void GrowAttack()
    {
        attack++;
        GameManager.instance.uiManager.UpdateAttackText(attack);
        UpdateStatusDisplay();
    }

    public void GrowSave()
    {
        maxSave++;
        UpdateStatusDisplay();
    }

    public void GrowHp()
    {
        maxHealth += 10;
        health += 10;
        UpdateStatusDisplay();
    }

    void Awake()
    {
        UpdateStatusDisplay();

        GameManager.instance.uiManager.UpdateCoinText(gold);
        GameManager.instance.uiManager.UpdateExpText(exp, levelUpExp[level - 1]);
        GameManager.instance.uiManager.UpdateLevelText(level);
        GameManager.instance.uiManager.UpdateAttackText(attack);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddGold(100);
        }
    }
}
