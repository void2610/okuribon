using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyContainer : MonoBehaviour
{
    [System.Serializable]
    internal class EnemyData
    {
        public GameObject prefab;
        public float probability;
    }

    [SerializeField]
    private List<EnemyData> enemies = new List<EnemyData>();
    [SerializeField]
    private List<EnemyData> bosses = new List<EnemyData>();
    private List<GameObject> currentEnemies = new List<GameObject>();
    private int enemyNum = 3;
    [SerializeField]
    private float alignment = 5;

    private List<Vector3> positions = new List<Vector3>();

    public int GetEnemyCount()
    {
        return currentEnemies.Count;
    }

    public List<EnemyBase> GetAllEnemies()
    {
        List<EnemyBase> enemyBases = new List<EnemyBase>();
        foreach (GameObject enemy in currentEnemies)
        {
            enemyBases.Add(enemy.transform.GetChild(0).GetComponent<EnemyBase>());
        }
        return enemyBases;
    }

    public void SpawnBoss()
    {
        float total = 0;
        foreach (EnemyData enemyData in bosses)
        {
            total += enemyData.probability;
        }
        float randomPoint = GameManager.instance.RandomRange(0.0f, total);

        foreach (EnemyData enemyData in bosses)
        {
            if (randomPoint < enemyData.probability)
            {
                var e = Instantiate(enemyData.prefab, this.transform);
                currentEnemies.Add(e);
                e.transform.position = positions[1];
                break;
            }
            randomPoint -= enemyData.probability;
        }
    }

    public void SpawnEnemy(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (currentEnemies.Count >= enemyNum) return;
            float total = 0;
            foreach (EnemyData enemyData in enemies)
            {
                total += enemyData.probability;
            }
            float randomPoint = GameManager.instance.RandomRange(0.0f, total);

            foreach (EnemyData enemyData in enemies)
            {
                if (randomPoint < enemyData.probability)
                {
                    var e = Instantiate(enemyData.prefab, this.transform);
                    currentEnemies.Add(e);
                    e.transform.position = positions[currentEnemies.Count - 1];
                    break;
                }
                randomPoint -= enemyData.probability;
            }
        }
    }

    public void SpawnEnemyByBoss(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (currentEnemies.Count >= enemyNum) return;
            float total = 0;
            foreach (EnemyData enemyData in enemies)
            {
                total += enemyData.probability;
            }
            float randomPoint = GameManager.instance.RandomRange(0.0f, total);

            foreach (EnemyData enemyData in enemies)
            {
                if (randomPoint < enemyData.probability)
                {
                    var e = Instantiate(enemyData.prefab, this.transform);
                    currentEnemies.Add(e);
                    if (currentEnemies.Count == 2)
                        e.transform.position = positions[0];
                    else if (currentEnemies.Count == 3)
                        e.transform.position = positions[2];
                    break;
                }
                randomPoint -= enemyData.probability;
            }
        }
    }

    public void AttackPlayer(Player player)
    {
        StartCoroutine(AttackPlayerCoroutine(player));
    }

    private IEnumerator AttackPlayerCoroutine(Player player)
    {
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            GameObject enemy = currentEnemies[i];
            // 敵を左側に移動させてゆっくり戻す
            enemy.transform.GetChild(0).transform.DOMoveX(-0.75f, 0.02f).SetRelative(true).OnComplete(() =>
            {
                enemy.transform.GetChild(0).transform.DOMoveX(0.75f, 0.2f).SetRelative(true).SetEase(Ease.OutExpo);
            });
            SeManager.instance.PlaySe("enemyAttack");
            EnemyBase enemyBase = enemy.transform.GetChild(0).GetComponent<EnemyBase>();
            enemyBase.Attack(player);

            // 0.5秒待つ
            yield return new WaitForSeconds(0.5f);
        }
        GameManager.instance.ChangeState(GameManager.GameState.PlayerTurn);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        GameManager.instance.player.AddExp(enemy.GetComponent<EnemyBase>().exp);
        GameManager.instance.player.AddGold(enemy.GetComponent<EnemyBase>().gold);
        GameObject g = enemy.transform.parent.gameObject;
        currentEnemies.Remove(g);
        enemy.GetComponent<EnemyBase>().OnDisappear();
    }

    void Awake()
    {
        positions.Add(this.transform.position + new Vector3(-alignment, 0, 0));
        positions.Add(this.transform.position);
        positions.Add(this.transform.position + new Vector3(alignment, 0, 0));
    }
    void Update()
    {
        // エディタだけ
        if (!Application.isEditor) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
    }
}
