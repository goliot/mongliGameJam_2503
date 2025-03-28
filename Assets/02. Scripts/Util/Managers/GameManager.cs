using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int StageLevel;
    public int SpawnCount;
    public Spawner spawner;
    public Player player;

    private void Start()
    {
        spawner.Spawn(spawner.GetRandomPosition(), SpawnCount);
    }

    public void Victory()
    {

    }

    public void GameOver()
    {

    }
}
