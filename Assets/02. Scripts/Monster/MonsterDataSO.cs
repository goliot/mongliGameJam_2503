using UnityEngine;

[CreateAssetMenu(fileName = "MonsterDataSO", menuName = "Scriptable Objects/MonsterDataSO")]
public class MonsterDataSO : ScriptableObject
{
    //���� �ִ�ü��
    public float maxHealth;
    //���� ü��
    public float health;
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
    
}
