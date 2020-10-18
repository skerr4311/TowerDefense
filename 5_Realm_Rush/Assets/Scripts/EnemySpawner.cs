using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [SerializeField] float secondsBetweenSpawn = 4f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text Score;
    [SerializeField] AudioClip spawnedEnemySFX;

    int enemysSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        Score.text = enemysSpawned.ToString();
        StartCoroutine(RepeatedlySpawnEnemies());
    }

    IEnumerator RepeatedlySpawnEnemies()
    {
        while (true)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParentTransform;
            AddScore();

            yield return new WaitForSeconds(secondsBetweenSpawn);
        }
    }

    private void AddScore()
    {
        enemysSpawned++;
        Score.text = enemysSpawned.ToString();
    }
}
