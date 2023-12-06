using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] private GameObject bullet;
    private Vector2 shotDir = Vector2.zero;
    private bool shotByPlayer;
    [SerializeField] private float bulletSpeed = 10f;
    private float enemyShootingCooldown = 2f;

    // Make a gun system
    // Define better methods for Getting components of gameobjects - either define a fixed system - like sprite comes after this or that/ or get the components using names

    private void FixedUpdate() {
        if (this.gameObject.CompareTag("Bullet")) {
            transform.position += new Vector3(shotDir.x, shotDir.y, 0) * bulletSpeed * Time.fixedDeltaTime;

            Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            if ((Mathf.Abs(transform.position.x) > screenBounds.x) || (Mathf.Abs(transform.position.y) > screenBounds.y)) {
                Destroy(this.gameObject);
            }
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

    public void ShootGun (GameObject gun) {
        GameObject spawnedBullet = Instantiate(bullet);

        Vector2 gunDir = gun.GetComponent<GunController>().GetGunDirection();
        // Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        // Vector2 bulletDir = new Vector2(mouseLocation.x - player.position.x, mouseLocation.y - player.position.y).normalized;
        spawnedBullet.GetComponent<BulletController>().shotDir = gunDir;

        spawnedBullet.transform.position = gun.transform.GetChild(0).GetChild(0).position;
        // float spawnOffset = (float) 1; // Temporary until a gun system is made
        // spawnedBullet.transform.position = new Vector3(gun.transform.position.x + spawnOffset * gunDir.x, gun.transform.position.y + spawnOffset * gunDir.y);

        spawnedBullet.GetComponent<BulletController>().shotByPlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D character) {
        if (this.gameObject.CompareTag("Bullet")) {
            if ((character.gameObject.CompareTag("Player") && !shotByPlayer) || (character.gameObject.CompareTag("Enemy") && shotByPlayer)) {
                Destroy(this.gameObject);
            }
        }
    }
}
