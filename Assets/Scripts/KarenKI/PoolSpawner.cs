using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PoolSpawner : MonoBehaviour
{
    public string playerTag = "Player";
    [HideInInspector] public GameObject player;

    [SerializeField] GameObject enemyPrefab;

    [SerializeField] int enemyStartAmount;
    [SerializeField] int enemyRefillAmount;
    [SerializeField] int maxEnemyAmount;

    [SerializeField] Vector3 cachePosition;

    [SerializeField] float minEnemySpawnDistance;

    private List<GameObject> activeEnemyList = new List<GameObject>();
    private List<GameObject> cacheEnemyList = new List<GameObject>();

    private float timer;
    [SerializeField] public float spawnRate;
    void Start()
    {
        player = GameObject.FindWithTag(playerTag);
        InstantiateNewEnemies(enemyStartAmount);
    }

    void Update()
    {

        if (activeEnemyList.Count < maxEnemyAmount)
        {
            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                timer = 0;
                SpawnNewEnemy();
            }
        }
    }

    public void SpawnNewEnemy()
    {
        GameObject newEnemy;
        if (cacheEnemyList.Count <= 0)
        {
            InstantiateNewEnemies(enemyRefillAmount);
        }
        newEnemy = cacheEnemyList.First();
        Vector3 spawnPosition = GetNewSpawnPosition();
        if (spawnPosition == Vector3.zero)
        {
            return;
        }
        newEnemy.transform.position = spawnPosition;
        cacheEnemyList.Remove(newEnemy);
        newEnemy.SetActive(true);
        activeEnemyList.Add(newEnemy);
    }
    private void InstantiateNewEnemies(int amount)
    {
        for (int i = 0; i < enemyStartAmount; i++)
        {
            GameObject newEnemy;
            newEnemy = Instantiate(enemyPrefab, cachePosition, transform.rotation, transform);
            newEnemy.SetActive(false);
            newEnemy.GetComponent<KarenBehavior>().spawner = this;
            cacheEnemyList.Add(newEnemy);
        }
    }
    public void DespawnEnemy(GameObject _enemy)
    {
        activeEnemyList.Remove(_enemy);

        _enemy.transform.position = cachePosition;
        _enemy.SetActive(false);
        cacheEnemyList.Add(_enemy);
    }

    private Vector3 GetNewSpawnPosition()
    {
        Vector3 spawnPosition;
        System.Random rnd = new System.Random();

        Vector3 playerPos = player.transform.position;


        if (rnd.Next(2) == 0)
        {
            spawnPosition = Vector3.right;
        }
        else
        {
            spawnPosition = Vector3.left;
        }

        spawnPosition = spawnPosition * minEnemySpawnDistance;

        spawnPosition = spawnPosition * ((float)rnd.Next(10, 20) / 10);

        spawnPosition = spawnPosition + playerPos;

        return spawnPosition;
    }
}