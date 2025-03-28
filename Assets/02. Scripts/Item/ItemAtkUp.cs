using UnityEngine;

public class ItemAtkUp : ItemRoot
{
    public override void Effect()
    {
        GetComponent<AudioSource>().Play();
        PlayerObject.GetComponent<Player>().PlayerData.NowDamage *= 1.2f;
    }
}
