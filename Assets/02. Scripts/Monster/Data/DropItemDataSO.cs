using UnityEngine;

[CreateAssetMenu(fileName = "DropItemDataSO", menuName = "Scriptable Objects/DropItemDataSO")]
public class DropItemDataSO : ScriptableObject
{
    public EObjectType[] Items;
}
