using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 60f)]
    [SerializeField] float secondsBetweenSpawns = 3f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform parent;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = parent;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }

    }
}
