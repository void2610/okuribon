using UnityEngine;

public class Portion : ItemBase
{
    public override void Use(Player p)
    {
        p.Heal(10);
    }

    protected override void Awake()
    {
        itemName = "Portion";
        price = 10;
        description = "Restores 10 HP";

        base.Awake();
    }
}
