using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public float DeadTime;

    public int StageLevel;
    public MobCountDataSO MobCountData;
    public Spawner spawner;
    public Player player;

    public Action GameOverAction;
    public bool IsGameOver = false;

    public int LeftMobCount;

    private void Start()
    {
        LeftMobCount = MobCountData.MobCountList[StageLevel];
        IsGameOver = false;
        spawner.Spawn(spawner.GetRandomPosition(), MobCountData.MobCountList[StageLevel]);
        UIManager.Instance.SetMonsterCount(MobCountData.MobCountList[StageLevel]);
    }

    private void Update()
    {
        DeadTime -= Time.deltaTime;
        if (DeadTime <= 0)
        {
            GameOver();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            GameOver();
        }

        if(LeftMobCount <= 0)
        {
            Victory();
        }
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
        IsGameOver = true;

        GameOverAction?.Invoke();

        player.GetComponent<Animator>().SetTrigger("Die");
    }
}
