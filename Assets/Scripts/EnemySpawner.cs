using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 60f)]
    [SerializeField] float secondsBetweenSpawns = 3f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform parent;
    [SerializeField] Text scoreText;
    int score = 0;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        scoreText.text = score.ToString();

    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            AddScore();
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = parent;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }

    }
    public void AddScore()
    {
        score ++;
        scoreText.text = score.ToString();
    }
}
