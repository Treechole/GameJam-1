using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodGenerator : MonoBehaviour {
    private ParticleSystem bloodGen;
    private bool startedSpurt = false;

    private void Awake() {
        bloodGen = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (bloodGen.isPlaying) {
            startedSpurt = true;
        } else {   
            if (startedSpurt) {
                Destroy(this.gameObject);
            }
        }
    }

    public void BloodSpurt() {
        Transform newBloodGen = Instantiate(bloodGen, this.transform.parent).gameObject.transform;
        newBloodGen.name = "Blood Particle System";
        newBloodGen.localPosition = Vector3.zero;

        this.transform.parent = null;
        bloodGen.Play();
    }
}
