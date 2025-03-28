using UnityEngine;

public class ItemSpeedUp : ItemRoot
{
    public override void Effect()
    {
        GetComponent<AudioSource>().Play();

        PlayerObject.GetComponent<Player>().PlayerData.NowSpeed *= 1.2f;
    }
}