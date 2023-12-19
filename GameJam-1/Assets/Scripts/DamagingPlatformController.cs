using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingPlatformController : MonoBehaviour {
    private float damagePerBlow = 10f;
    private float secondsPerBlow = 2f;
    private bool initiatedDamage = false;

    private void Update() {
        Debug.Log(initiatedDamage);
    }

    private void OnTriggerStay2D(Collider2D character) {
        if (character.gameObject.CompareTag("Player")) {
            if (!initiatedDamage) {
                RecursiveDamage(character.gameObject);
            }
            initiatedDamage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D character) {
        if (character.gameObject.CompareTag("Player")) {
            initiatedDamage = false;
        }
    }

    private void RecursiveDamage(GameObject player) {
        player.GetComponent<HealthController>().DamageDealt(damagePerBlow);
        StartCoroutine(RechargeWait(player));
    }

    private IEnumerator RechargeWait(GameObject player) {
        yield return new WaitForSeconds(secondsPerBlow);
        if (initiatedDamage) {
                RecursiveDamage(player);
            }
    }
}
