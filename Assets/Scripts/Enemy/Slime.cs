using UnityEngine;

public class Slime : EnemyBase
{
    private void SlimeAttack(Player player)
    {
        player.AddSave(-attack);
    }

    protected override void Awake()
    {
        enemyName = "Slime";
        health = 10;
        maxHealth = 10;
        attack = 1;
        defense = 0;
        gold = 1;
        exp = 10;
        enemyActions.Add(new AttackData
        {
            name = "SlimeAttack",
            action = SlimeAttack,
            probability = 0.2f,
            color = Color.blue,
            description = "nullnull",
        });

        base.Awake();
    }
}
