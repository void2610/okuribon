using UnityEngine;
using DG.Tweening;

public class Slime : EnemyBase
{
    private void SlimeAttack(Player player)
    {
        player.AddSaveFromEnemy(-attack);
    }

    protected override void Awake()
    {
        enemyName = "タマシイ";
        health = 10;
        maxHealth = 10;
        attack = 1;
        defense = 0;
        gold = 2;
        exp = 10;
        enemyActions.Add(new AttackData
        {
            name = "すいとる",
            action = SlimeAttack,
            probability = 0.2f,
            color = Color.blue,
            description = "ためた値を1へらす",
        });

        base.Awake();

        // ゆらゆらと動かす
        transform.DOLocalMoveY(0.2f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
