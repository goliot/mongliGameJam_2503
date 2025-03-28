using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Scriptable Objects/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    public float MaxHp;
    public float MaxSheild;
    public int MaxBullet;
    public float OriginalDamage;
    public float NowDamage;
    public float OriginalSpeed;
    public float NowSpeed;
    public float OriginalAtkSpeed;
    public float NowAtkSpeed;
}