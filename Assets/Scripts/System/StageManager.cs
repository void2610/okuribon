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

    public void Start()
    {
        GameManager.instance.uiManager.UpdateStageText(currentStage);
    }
}
