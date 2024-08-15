using UnityEngine;

public class Portion : ItemBase
{
    public override void Use(Player p)
    {
        p.Heal(10);
    }

    protected override void Awake()
    {
        itemName = "あかいおふだ";
        price = 3;
        description = "たいりょくを\n10かいふくする";

        base.Awake();
    }
}
