using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] private GameObject bullet;
    private float bulletSpeed = 10f;
    private float enemyShootingCooldown = 2f;

    public IEnumerator ShootPlayer (Transform player, Transform enemy) {
        while (true) {
            yield return new WaitForSeconds(enemyShootingCooldown);

            GameObject spawnedBullet = Instantiate(bullet, enemy);
            spawnedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(player.position.x - enemy.position.x, player.position.y - enemy.position.y).normalized * bulletSpeed;
            // Correct the bullet depending on player position even after release
        }
    }

    public void ShootBullet (Transform player) {
        GameObject spawnedBullet = Instantiate(bullet, player);
        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        spawnedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(mouseLocation.x - player.position.x, mouseLocation.y - player.position.y).normalized * bulletSpeed;        
        // Define the direction of bullet shooting
    }
}
