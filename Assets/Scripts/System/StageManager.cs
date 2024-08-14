using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public enum StageType
    {
        enemy,
        shop,
        boss,
        events,
        other,
    }

    [SerializeField]
    private int enemyStageNum = 3;


    public List<StageType> stageTypes = new List<StageType>();
    private int currentStage = -1;

    public void NextStage()
    {
        SeManager.instance.PlaySe("footsteps");
        //背景をスクロールさせる
        Utils.instance.WaitAndInvoke(2.0f, () =>
        {
            currentStage++;
            if (currentStage >= stageTypes.Count)
            {
                currentStage = 0;
            }
            GameManager.instance.uiManager.UpdateStageText(currentStage + 1);

            switch (stageTypes[currentStage])
            {
                case StageType.enemy:
                    GameManager.instance.enemyContainer.SpawnEnemy();
                    GameManager.instance.ChangeState(GameManager.GameState.PlayerTurn);
                    break;
                case StageType.shop:
                    GameManager.instance.ChangeState(GameManager.GameState.Shop);
                    break;
                case StageType.boss:
                    GameManager.instance.enemyContainer.SpawnBoss();
                    GameManager.instance.ChangeState(GameManager.GameState.PlayerTurn);
                    break;
                case StageType.events:
                    GameManager.instance.ChangeState(GameManager.GameState.Other);
                    break;
                case StageType.other:
                    GameManager.instance.ChangeState(GameManager.GameState.Other);
                    break;
            }
        });
    }

    private void DecideStage()
    {
        int shopNum = enemyStageNum / 2;
        stageTypes.Clear();
        for (int i = 0; i < enemyStageNum + shopNum; i++) stageTypes.Add(StageType.other);
        Debug.Log("stageTypes.Count: " + stageTypes.Count);
        for (int i = 0; i < shopNum; i++)
        {
            int index = GameManager.instance.RandomRange(1, stageTypes.Count);
            if (stageTypes[index] == StageType.shop)
            {
                i--;
                continue;
            }
            stageTypes[index] = StageType.shop;
        }
        Debug.Log("stageTypes.Count: " + stageTypes.Count);

        for (int i = 0; i < stageTypes.Count; i++)
        {
            if (stageTypes[i] == StageType.other)
            {
                stageTypes[i] = StageType.enemy;
            }
        }
        stageTypes.Add(StageType.boss);
    }

    public void Start()
    {
        DecideStage();
        GameManager.instance.uiManager.UpdateStageText(currentStage);
    }
}
