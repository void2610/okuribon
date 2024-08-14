using UnityEngine;
using DG.Tweening;

public class Boss : EnemyBase
{
    private void BossAttack(Player player)
    {
        //なんか攻撃する
    }

    protected override void Awake()
    {
        enemyName = "???";
        health = 50;
        maxHealth = 50;
        attack = 5;
        defense = 0;
        gold = 50;
        exp = 0;
        enemyActions.Add(new AttackData
        {
            name = "ボスこうげき",
            action = BossAttack,
            probability = 0.2f,
            color = Color.black,
            description = "つよいよ",
        });

        base.Awake();

        // ゆらゆらと動かす
        transform.DOLocalMoveY(0.2f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
