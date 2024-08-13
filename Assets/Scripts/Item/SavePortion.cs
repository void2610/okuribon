using UnityEngine;

public class SavePortion : ItemBase
{
    public override void Use(Player p)
    {
        p.AddSaveFromItem(10);
    }

    protected override void Awake()
    {
        itemName = "Save Portion";
        price = 5;
        description = "Add 10 Save";

        base.Awake();
    }
}
