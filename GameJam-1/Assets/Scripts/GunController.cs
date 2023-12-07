using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    private Vector2 gunDir;
    private float gunOffset = 1/Mathf.Sqrt(2);

    private void Update() {
        if (this.gameObject.transform.parent != null) {
            if (this.gameObject.CompareTag("Gun") && this.gameObject.transform.parent.CompareTag("Player")) {
                Transform player = this.gameObject.transform.parent;

                SetGunDirection(player);

                Transform gunSprite = transform.Find("Sprite");

                if (gunDir.x * gunSprite.localScale.x < 0) {
                    gunSprite.localScale = new Vector3(-gunSprite.localScale.x, gunSprite.localScale.y, gunSprite.localScale.z);
                }

                gameObject.transform.position = new Vector3(player.position.x + gunOffset * gunDir.x, player.position.y + gunOffset * gunDir.y);
                gunSprite.rotation = Quaternion.Euler(0, 0, Mathf.Atan(gunDir.y/gunDir.x) * (180/Mathf.PI));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D character) {
        if (character.gameObject.CompareTag("Player")) {
            if (!character.gameObject.GetComponent<PlayerController>().CheckGun()) {
                Transform player = character.gameObject.transform;
                player.GetComponent<PlayerController>().SetGun();

                gameObject.transform.SetParent(player);

                SetGunDirection(player);

                gameObject.transform.position = new Vector3(character.transform.position.x + gunOffset * gunDir.x, character.transform.position.y + gunOffset * gunDir.y);
                gameObject.GetComponent<AmmoController>().UpdateAmmoInfo();
            }
        }
    }

    private void SetGunDirection (Transform player) {
        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        gunDir = new Vector2(mouseLocation.x - player.position.x, mouseLocation.y - player.position.y).normalized;
    }

    public Vector2 GetGunDirection () {
        return gunDir;
    }
}
