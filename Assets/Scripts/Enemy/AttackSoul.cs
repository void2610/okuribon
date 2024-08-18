using UnityEngine;
using DG.Tweening;

public class AttackSoul : EnemyBase
{
    private void SlimeAttack(Player player)
    {
        player.AddSaveFromEnemy(-attack);
    }

    protected override void Awake()
    {
        enemyName = "あかい\nタマシイ";
        hMax = 8;
        hMin = 5;
        attack = 3;
        defense = 0;
        gold = 3;
        exp = 20;
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
