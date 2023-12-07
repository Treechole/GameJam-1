using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoController : MonoBehaviour {
    private float maximumAmmo = 10f;
    private float currentBullets;
    private bool hasAmmo = true;
    private TextMeshProUGUI ammoText;
    private float ammoRecharge = 5;

    private void Awake() {
        currentBullets = maximumAmmo;
        ammoText = GameObject.Find("Ammunation Text").GetComponent<TextMeshProUGUI>();
        if (this.gameObject.CompareTag("Gun")) {
            if (this.gameObject.transform.parent != null) { // the if statement below is not necessary as per the current system
                if (this.gameObject.transform.parent.GetComponent<PlayerController>().CheckGun()) {
                    UpdateAmmoInfo();
                }
            }
        }
    }

    public void AmmoFired () {
        currentBullets -= 1;

        if (currentBullets == 0) {
            hasAmmo = false;
        }

        UpdateAmmoInfo();
    }

    // Either keep checkAmmo or hasAmmo

    public bool checkAmmo () {
        if (currentBullets > 0) {
            hasAmmo = true;
        } else {
            hasAmmo = false;
        }
        return hasAmmo;
    }

    private void OnTriggerEnter2D(Collider2D character) {
        if (this.gameObject.CompareTag("Ammunation") && character.gameObject.CompareTag("Player")) {
            if (character.gameObject.GetComponent<PlayerController>().CheckGun()) {
                Destroy(this.gameObject);
                GameObject gun = character.gameObject.GetComponent<PlayerController>().FindWithTag(character.gameObject, "Gun");
                
                gun.GetComponent<AmmoController>().currentBullets += ammoRecharge;
                if (gun.GetComponent<AmmoController>().currentBullets > maximumAmmo) {
                    gun.GetComponent<AmmoController>().currentBullets = maximumAmmo;
                }

                hasAmmo = gun.GetComponent<AmmoController>().checkAmmo();
                gun.GetComponent<AmmoController>().UpdateAmmoInfo();
            }
        }
    }

    public void UpdateAmmoInfo () {
        ammoText.SetText("Ammo: {0}/{1}", currentBullets, maximumAmmo);
    }
}
