using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    private Vector3 screenBounds;
    [SerializeField] private int maximumEnemies = 6;
    private float spawnDuration = 2f;
    private float spawnBound = 2f;


    private void Awake() {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy() {
        int enemyCount = 0;
        while (enemyCount < maximumEnemies) {
            yield return new WaitForSeconds(spawnDuration);
            
            GameObject spawnedEnemy = Instantiate(enemy);

            float spawnRadius = Random.Range(spawnBound, screenBounds.y);
            float spawnAngle = Random.Range(-Mathf.PI, Mathf.PI);
            
            spawnedEnemy.transform.position = new Vector3(spawnRadius * Mathf.Cos(spawnAngle), spawnRadius * Mathf.Sin(spawnAngle), 0);
            spawnedEnemy.transform.rotation = Quaternion.Euler(0, 0, (Mathf.PI - spawnAngle) * (180/Mathf.PI));

            enemyCount++;
        }
    }
}
