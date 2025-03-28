using UnityEngine;

public class ItemAtkUp : ItemRoot
{
    public override void Effect()
    {
        PlayerObject.GetComponent<Player>().PlayerData.NowDamage *= 1.2f;
    }
}
