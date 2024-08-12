using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            enemyBases.Add(enemy.GetComponent<EnemyBase>());
        }
        return enemyBases;
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

    public void AttackPlayer(Player player)
    {
        foreach (GameObject enemy in currentEnemies)
        {
            enemy.GetComponent<EnemyBase>().Attack(player);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        GameManager.instance.player.AddExp(enemy.GetComponent<EnemyBase>().exp);
        GameManager.instance.player.AddGold(enemy.GetComponent<EnemyBase>().gold);
        currentEnemies.Remove(enemy);
        Destroy(enemy);
    }

    void Start()
    {
        positions.Add(this.transform.position + new Vector3(-alignment, 0, 0));
        positions.Add(this.transform.position);
        positions.Add(this.transform.position + new Vector3(alignment, 0, 0));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
    }
}
