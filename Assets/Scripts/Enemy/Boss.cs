using UnityEngine;

public class Boss : EnemyBase
{
    private void BossAttack(Player player)
    {
        //なんか攻撃する
    }

    protected override void Awake()
    {
        enemyName = "Boss";
        health = 100;
        maxHealth = 100;
        attack = 10;
        defense = 0;
        gold = 100;
        exp = 100;
        enemyActions.Add(new AttackData
        {
            name = "BossAttack",
            action = BossAttack,
            probability = 0.2f,
            color = Color.black,
            description = "tuyoi",
        });

        base.Awake();
    }
}
