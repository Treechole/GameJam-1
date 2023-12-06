using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
    private float health = 100;
    private GameObject healthBarSprite;
    private TextMeshProUGUI healthBarText;
    private Color fullHealthColor = new Color(0, (float) 0.7058824, 0, 1);
    private Color halfHealthColor = new Color((float)0.7058824, (float)0.7058824, 0, 1);
    private Color noHealthColor = new Color((float) 0.7058824, 0, 0, 1);

    private void Awake() {
        healthBarSprite = transform.Find("Health Bar/Sprite").gameObject;
        healthBarText = transform.Find("Health Bar/Text Canvas/Health").gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void DamageDealt(float damage) {
        if (health != 0) {
            health -= damage;
            UpdateHealthBar();

            if (health == 0) {
                Debug.Log("Game Over!");
            }
        }
    }

    private void UpdateHealthBar () {
        healthBarSprite.transform.localScale = new Vector3(health/100, healthBarSprite.transform.localScale.y, healthBarSprite.transform.localScale.z);

        if (health >= 50) {
            healthBarSprite.GetComponent<SpriteRenderer>().color = Color.Lerp(halfHealthColor, fullHealthColor, (health/50) - 1);
        } else {
            healthBarSprite.GetComponent<SpriteRenderer>().color = Color.Lerp(noHealthColor, halfHealthColor, health/50);
        }

        healthBarText.SetText(health.ToString());
    }
}
