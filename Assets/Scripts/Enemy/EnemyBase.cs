using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public string enemyName = "Enemy";
    public int health = 100;
    public int maxHealth = 100;
    public int attack = 2;
    public int defense = 1;
    public int gold = 0;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Death();
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

    public void Attack(Player player)
    {
        player.TakeDamage(attack);
    }

    public void Death()
    {
        this.transform.parent.GetComponent<EnemyContainer>().RemoveEnemy(this.gameObject);
    }
}
