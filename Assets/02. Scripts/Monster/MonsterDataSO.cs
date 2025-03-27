using UnityEngine;

[CreateAssetMenu(fileName = "MonsterDataSO", menuName = "Scriptable Objects/MonsterDataSO")]
public class MonsterDataSO : ScriptableObject
{
    //몬스터 최대체력
    public float maxHealth;
    //몬스터 체력
    public float health;
    //몬스터 공격력
    public float attackPower;
    //몬스터 이동속도
    public float moveSpeed;
    //공격 쿨타임
    public float waitTime;
    //공격 범위
    public float attackRange;
    //추적 범위
    public float chaseRange;
    
}
