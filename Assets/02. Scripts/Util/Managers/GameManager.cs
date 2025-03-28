using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int StageLevel;
    public MobCountDataSO MobCountData;
    public Spawner spawner;
    public Player player;

    private void Start()
    {
        spawner.Spawn(spawner.GetRandomPosition(), MobCountData.MobCountList[StageLevel]);
    }

    public void Victory()
    {

    }

    public void GameOver()
    {

    }
}
