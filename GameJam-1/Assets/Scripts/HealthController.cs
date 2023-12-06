using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
    private float maxHealth;
    private float health;
    private GameObject healthBarSprite;
    private Color fullHealthColor = new Color(0, (float) 0.7058824, 0, 1);
    private Color halfHealthColor = new Color((float)0.7058824, (float)0.7058824, 0, 1);
    private Color noHealthColor = new Color((float) 0.7058824, 0, 0, 1);

    private void Awake() {
        if (this.gameObject.CompareTag("Player")) {
            maxHealth = GetComponent<PlayerController>().GetMaxHealth();
        } else if (this.gameObject.CompareTag("Enemy")) {
            maxHealth = GetComponent<EnemyController>().GetMaxHealth();
        }
        health = maxHealth;
        healthBarSprite = transform.Find("Health Bar/Sprite").gameObject;
    }

    public void DamageDealt(float damage) {
        if (health != 0) {
            health -= damage;
            UpdateHealthBar();

            if (health == 0) {
                CharcterDied();
            }
        }
    }

    private void UpdateHealthBar () {
        healthBarSprite.transform.localScale = new Vector3(health/maxHealth, healthBarSprite.transform.localScale.y, healthBarSprite.transform.localScale.z);

        if (health >= maxHealth/2) {
            healthBarSprite.GetComponent<SpriteRenderer>().color = Color.Lerp(halfHealthColor, fullHealthColor, (2*health/maxHealth) - 1);
        } else {
            healthBarSprite.GetComponent<SpriteRenderer>().color = Color.Lerp(noHealthColor, halfHealthColor, 2*health/maxHealth);
        }
    }

    private void CharcterDied () {
        if (this.gameObject.CompareTag("Player")) {
            Debug.Log("Game Over!");
        }
        Destroy(this.gameObject);
    }
}
