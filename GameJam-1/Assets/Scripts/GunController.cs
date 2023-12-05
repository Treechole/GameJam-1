using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    private void Update() {
        if (this.gameObject.transform.parent != null) {
            if (this.gameObject.CompareTag("Gun") && this.gameObject.transform.parent.CompareTag("Player")) {
                Transform player = this.gameObject.transform.parent;

                Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
                Vector2 gunDir = new Vector2(mouseLocation.x - player.position.x, mouseLocation.y - player.position.y).normalized;

                Transform gunSprite = transform.GetChild(0);

                if ((gunDir.x < 0 && gunSprite.localScale.x > 0) || (gunDir.x > 0 && gunSprite.transform.localScale.x < 0)) {
                    gunSprite.localScale = new Vector3(-gunSprite.localScale.x, gunSprite.localScale.y, gunSprite.localScale.z);
                }

                float gunOffset = 1; ///Mathf.Sqrt(2);
                gameObject.transform.position = new Vector3(player.position.x + gunOffset * gunDir.x, player.position.y + gunOffset * gunDir.y);
                // Debug.Log(Mathf.Atan(gunDir.y/gunDir.x));
                gunSprite.rotation = Quaternion.Euler(0, 0, Mathf.Atan(gunDir.y/gunDir.x) * (180/Mathf.PI));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D character) {
        if (character.gameObject.CompareTag("Player")) {
            gameObject.transform.SetParent(character.transform);

            Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            Vector2 gunDir = new Vector2(mouseLocation.x - character.transform.position.x, mouseLocation.y - character.transform.position.y).normalized;

            float gunOffset = 1/Mathf.Sqrt(2);
            gameObject.transform.position = new Vector3(character.transform.position.x + gunOffset * gunDir.x, character.transform.position.y + gunOffset * gunDir.y);
        }
    }
}
