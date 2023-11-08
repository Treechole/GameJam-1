using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeDisplayer : MonoBehaviour {

    private TextMeshProUGUI timer;
    private float timePassed = 0f;
    [SerializeField] private PlayerController player;

    private void Awake() {
        timer = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        timePassed += Time.deltaTime/player.GetTimeManipulation();
        processTime((int) Mathf.Floor(timePassed), out int minutes, out int seconds);
        string text = string.Format("{0}:{1}", addZero(minutes), addZero(seconds));
        timer.SetText(text);
    }

    private void processTime(int currentTime, out int minutes, out int seconds) {
        seconds = currentTime;
        minutes = 0;

        while (seconds >= 60) {
            seconds -= 60;
            minutes += 1;
        }
    }

    private string addZero(int time) {
        string final_time = time.ToString();

        if (time <= 9) {
            final_time = string.Format("0{0}", time);
        }

        return final_time;
    }

}
