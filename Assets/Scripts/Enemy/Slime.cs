using UnityEngine;

public class Slime : EnemyBase
{
    protected orverride Awake()
    {
        enemyName = "Slime";
        health = 10;
        maxHealth = 10;
        attack = 1;
        defense = 0;
        gold = 10;

        base.Awake();
    }
}
