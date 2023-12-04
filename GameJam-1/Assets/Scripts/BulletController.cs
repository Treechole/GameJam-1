using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] private GameObject bullet;
    private Vector2 shotDir = Vector2.zero;
    private bool shotByPlayer;
    [SerializeField] private float bulletSpeed = 10f;
    private float enemyShootingCooldown = 2f;

    // Set bullet rigidbody so that it cant interact as physics object
    // Set the player and enemy to rigidbodies

    private void FixedUpdate() {
        if (this.gameObject.CompareTag("Bullet")) {
            transform.position += new Vector3(shotDir.x, shotDir.y, 0) * bulletSpeed * Time.fixedDeltaTime;
        }
    }

    public IEnumerator ShootPlayer (Transform player, Transform enemy) {
        while (true) {
            yield return new WaitForSeconds(enemyShootingCooldown);

            GameObject spawnedBullet = Instantiate(bullet);
            
            Vector2 bulletDir = new Vector2(player.position.x - enemy.position.x, player.position.y - enemy.position.y).normalized;
            spawnedBullet.GetComponent<BulletController>().shotDir = bulletDir;

            float spawnOffset = 1/Mathf.Sqrt(2); // Temporary until a gun system is made
            spawnedBullet.transform.position = new Vector3(enemy.position.x + spawnOffset * bulletDir.x, enemy.position.y + spawnOffset * bulletDir.y, 0);

            spawnedBullet.GetComponent<BulletController>().shotByPlayer = false;
        }
    }

    public void ShootBullet (Transform player) {
        GameObject spawnedBullet = Instantiate(bullet);
        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

        Vector2 bulletDir = new Vector2(mouseLocation.x - player.position.x, mouseLocation.y - player.position.y).normalized;
        spawnedBullet.GetComponent<BulletController>().shotDir = bulletDir;

        float spawnOffset = 1/Mathf.Sqrt(2); // Temporary until a gun system is made
        spawnedBullet.transform.position = new Vector3(player.position.x + spawnOffset * bulletDir.x, player.position.y + spawnOffset * bulletDir.y, 0);

        spawnedBullet.GetComponent<BulletController>().shotByPlayer = true;
    }

    private void OnCollisionEnter2D(Collision2D character) {
        if (this.gameObject.CompareTag("Bullet")) {
            if ((character.gameObject.CompareTag("Player") && !shotByPlayer) || (character.gameObject.CompareTag("Enemy") && shotByPlayer)) {
                Destroy(this.gameObject);
            }
        }
    }
}
