using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeDisplayer : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timer;
    private float startTime;

    private void Awake() {
        timer = GetComponent<TextMeshProUGUI>();
        startTime = Time.time;
    }

    private void Update() {
        float timePassed = Time.time - startTime;
        timer.SetText(timePassed.ToString());
    }

}
