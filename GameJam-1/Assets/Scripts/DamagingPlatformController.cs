using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingPlatformController : MonoBehaviour {
    private float damagePerBlow = 10f;
    private float rechargeTime = 2f;
    private bool playerOnPlatform = false;
    private bool recharged = true;
    private bool currentlyRecharging = false;
    private GameObject player = null;

    private void Update() {
        if (playerOnPlatform) {
            DamagePlayer();
        }

        if (!recharged && !currentlyRecharging) {
            StartCoroutine(RechargePlatform());
        }
    }

    private void OnTriggerEnter2D(Collider2D character) {
        if (character.CompareTag("Player")) {
            playerOnPlatform = true;
            player = character.gameObject;
        }
    }

    private void DamagePlayer() {
        if (recharged) {
            HealthController playerHealthSystem = player.GetComponent<HealthController>();

            playerHealthSystem.DealDamage(damagePerBlow);
            recharged = false;
        }
    }

    private void OnTriggerExit2D(Collider2D character) {
        if (character.CompareTag("Player")) {
            playerOnPlatform = false;
            player = null;
        }
    }

    private IEnumerator RechargePlatform() {
        currentlyRecharging = true;
        yield return new WaitForSeconds(rechargeTime);
        recharged = true;
        currentlyRecharging = false;
    }
}
