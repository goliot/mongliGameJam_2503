using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int StageLevel;
    public MobCountDataSO MobCountData;
    public Spawner spawner;
    public Player player;

    public Action GameOverAction;

    private void Start()
    {
        spawner.Spawn(spawner.GetRandomPosition(), MobCountData.MobCountList[StageLevel]);
        UIManager.Instance.SetMonsterCount(MobCountData.MobCountList[0]);
    }

    //다음 레벨로 갈 경우
    private void NextLevel()
    {
        UIManager.Instance.SetMonsterCount(MobCountData.MobCountList[StageLevel]);
    }

    public void Victory()
    {

    }

    public void GameOver()
    {
        GameOverAction?.Invoke();
    }
}
