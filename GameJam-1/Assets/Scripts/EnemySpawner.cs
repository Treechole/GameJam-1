using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    private Vector3 screenBounds;
    private float spawnDuration = 2f;

    private void Awake() {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy() {
        int i = 0;
        while (i != 5) {
            yield return new WaitForSeconds(spawnDuration);
            
            GameObject spawnedEnemy = Instantiate(enemy);
            spawnedEnemy.transform.position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y), 0);
            
            if (player.transform.position.x == spawnedEnemy.transform.position.x) {
                if (spawnedEnemy.transform.position.y > 0) {
                    spawnedEnemy.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            } else {
                int dir_rotation = -90;
                if (spawnedEnemy.transform.position.x > 0) {
                    dir_rotation = +90;
                }
                spawnedEnemy.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan((player.transform.position.y - spawnedEnemy.transform.position.y) / (player.transform.position.x - spawnedEnemy.transform.position.x))*(180/Mathf.PI) + dir_rotation);
            }

            i++;
        }
    }
}
