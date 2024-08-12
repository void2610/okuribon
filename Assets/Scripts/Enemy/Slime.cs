using UnityEngine;

public class Slime : EnemyBase
{
    protected override void Awake()
    {
        enemyName = "Slime";
        health = 10;
        maxHealth = 10;
        attack = 1;
        defense = 0;
        gold = 1;
        exp = 10;

        base.Awake();
    }
}
