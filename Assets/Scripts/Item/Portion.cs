using UnityEngine;

public class Portion : ItemBase
{
    public override void Use(Player p)
    {
        p.Heal(10);
    }

    protected override void Awake()
    {
        itemName = "赤いえきたい";
        price = 3;
        description = "たいりょくを10かいふく";

        base.Awake();
    }
}
