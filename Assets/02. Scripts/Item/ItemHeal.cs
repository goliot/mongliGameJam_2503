using UnityEngine;

public class ItemHeal : ItemRoot
{
    public override void Effect()
    {
        PlayerObject.GetComponent<Player>().Health += 40;
    }
}
