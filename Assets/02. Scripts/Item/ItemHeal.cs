using UnityEngine;

public class ItemHeal : ItemRoot
{
    public override void Effect()
    {
        GetComponent<AudioSource>().Play();

        PlayerObject.GetComponent<Player>().Health += 40;
    }
}
