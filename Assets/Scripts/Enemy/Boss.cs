using UnityEngine;
using DG.Tweening;

public class Boss : EnemyBase
{
    private void BossAttack(Player player)
    {
        //なんか攻撃する
    }

    protected virtual void PiercingAttack(Player player)
    {
        player.TakeDamage(attack / 2);
    }

    protected override void Awake()
    {
        enemyName = "???";
        health = 75;
        maxHealth = 75;
        attack = 5;
        defense = 0;
        gold = 50;
        exp = 0;

        enemyActions.Add(new AttackData
        {
            name = "かんつう\nこうげき",
            action = PiercingAttack,
            probability = 0.2f,
            color = Color.yellow,
            description = "ためることができない",
        });
        enemyActions.Add(new AttackData
        {
            name = "???",
            action = BossAttack,
            probability = 0.2f,
            //水色 #82dcff
            color = new Color(0.509f, 0.863f, 1f),
            description = "つよいよ",
        });

        base.Awake();

        // ゆらゆらと動かす
        transform.DOLocalMoveY(0.2f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
