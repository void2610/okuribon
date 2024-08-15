using UnityEngine;

public class SavePortion : ItemBase
{
    public override void Use(Player p)
    {
        p.AddSaveFromItem(10);
    }

    protected override void Awake()
    {
        itemName = "あおいおふだ";
        price = 5;
        description = "ようりょくを\n10ふやす";

        base.Awake();
    }
}
