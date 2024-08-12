using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (PlayerPrefs.GetInt("RandomSeed", 1) == 1)
            {
                seed = (int)DateTime.Now.Ticks;
                Debug.Log("Random");
            }
            else
            {
                seed = PlayerPrefs.GetInt("Seed", seed);
                Debug.Log("Seed: " + seed);
            }
            random = new System.Random(seed);
            DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 200, sequencesCapacity: 200);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public enum GameState
    {
        PlayerTurn,
        EnemyTurn,
        LevelUp,
        Shop,
        Paused,
        GameOver,
        Other
    }
    public GameState state = GameState.PlayerTurn;


    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private EnemyContainer enemyContainer;

    public System.Random random { get; private set; }
    private int seed = 42;
    private Player player => playerObj.GetComponent<Player>();
    private UIManager uiManager => GetComponent<UIManager>();

    public float RandomRange(float min, float max)
    {
        float randomValue = (float)(this.random.NextDouble() * (max - min) + min);
        return randomValue;
    }

    public int RandomRange(int min, int max)
    {
        int randomValue = this.random.Next(min, max);
        return randomValue;
    }


    public void OnClickAttack()
    {
        player.Attack(enemyContainer.GetAllEnemies());
        ChangeState(GameState.EnemyTurn);
    }
    public void OnClickSave()
    {
        player.isSaving = true;
        ChangeState(GameState.EnemyTurn);
    }
    public void OnClickEscape()
    {
        ChangeState(GameState.EnemyTurn);
    }

    public void ChangeState(GameState newState)
    {
        switch (state)
        {
            case GameState.PlayerTurn:
                uiManager.EnablePlayerActions(false);
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.LevelUp:
                break;
            case GameState.Shop:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
            case GameState.Other:
                break;
        }
        state = newState;
        Debug.Log("State: " + state);
        switch (newState)
        {
            case GameState.PlayerTurn:
                uiManager.EnablePlayerActions(true);
                player.isSaving = false;
                break;
            case GameState.EnemyTurn:
                enemyContainer.AttackPlayer(player);
                ChangeState(GameState.PlayerTurn);
                break;
            case GameState.LevelUp:
                break;
            case GameState.Shop:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
            case GameState.Other:
                break;
        }
    }

    void Start()
    {
        ChangeState(GameState.PlayerTurn);
    }

    void Update()
    {
        switch (state)
        {
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.LevelUp:
                break;
            case GameState.Shop:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
            case GameState.Other:
                break;
        }
    }
}
