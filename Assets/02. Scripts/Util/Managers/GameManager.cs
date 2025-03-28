using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int StageLevel;
    public MobCountDataSO MobCountData;
    public Spawner spawner;
    public Player player;

    public Action GameOverAction;
    public bool IsGameOver = false;

    private void Start()
    {
        IsGameOver = false;
        spawner.Spawn(spawner.GetRandomPosition(), MobCountData.MobCountList[StageLevel]);
        UIManager.Instance.SetMonsterCount(MobCountData.MobCountList[0]);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameOver();
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
