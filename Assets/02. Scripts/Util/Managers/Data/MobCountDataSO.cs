using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobCountData", menuName = "Scriptable Objects/MobCountData")]
public class MobCountDataSO : ScriptableObject
{
    public List<int> MobCountList;
    public List<EObjectType> WhichMob;
}
