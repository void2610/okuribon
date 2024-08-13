using UnityEngine;

public class SavePortion : ItemBase
{
    public override void Use(Player p)
    {
        p.AddSave(10);
    }

    protected override void Awake()
    {
        itemName = "SavePortion";
        price = 10;
        description = "Add 10 Save";

        base.Awake();
    }
}
