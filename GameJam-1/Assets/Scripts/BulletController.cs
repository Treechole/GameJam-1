using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 10f;
    private float enemyShootingCooldown = 2f;
    private bool shotByPlayer;

    // Set bullet rigidbody so that it cant interact as physics object
    // Set the player and enemy to rigidbodies

    public IEnumerator ShootPlayer (Transform player, Transform enemy) {
        while (true) {
            yield return new WaitForSeconds(enemyShootingCooldown);

            GameObject spawnedBullet = Instantiate(bullet, enemy);
            // spawnedBullet.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1, 0);
            spawnedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(player.position.x - enemy.position.x, player.position.y - enemy.position.y).normalized * bulletSpeed;
            // Correct the bullet depending on player position even after release

            spawnedBullet.GetComponent<BulletController>().shotByPlayer = false;
        }
    }

    public void ShootBullet (Transform player) {
        GameObject spawnedBullet = Instantiate(bullet, player);
        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        spawnedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(mouseLocation.x - player.position.x, mouseLocation.y - player.position.y).normalized * bulletSpeed;
        // Define the direction of bullet shooting

        spawnedBullet.GetComponent<BulletController>().shotByPlayer = true;
    }

    private void OnCollisionEnter2D(Collision2D character) {
        if (this.gameObject.CompareTag("Bullet")) {
            Debug.Log(string.Format("Shot by Player: {0}, Hit at {1}", shotByPlayer, character.gameObject.tag));
            if ((character.gameObject.CompareTag("Player") && !shotByPlayer) || (character.gameObject.CompareTag("Enemy") && shotByPlayer)) {
                Destroy(this.gameObject);
            }
        }
    }
}
