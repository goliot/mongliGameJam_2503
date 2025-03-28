using UnityEngine;

[CreateAssetMenu(fileName = "MonsterDataSO", menuName = "Scriptable Objects/MonsterDataSO")]
public class MonsterDataSO : ScriptableObject
{
    //���� �ִ�ü��
    public float maxHealth;
    //���� ���ݷ�
    public float attackPower;
    //���� �̵��ӵ�
    public float moveSpeed;
    //���� ��Ÿ��
    public float waitTime;
    //���� ����
    public float attackRange;
    //���� ����
    public float chaseRange;
    //���� �Ÿ�
    public float chargeDistance;
    //���� �ӵ�
    public float chargeSpeed;
    
}
