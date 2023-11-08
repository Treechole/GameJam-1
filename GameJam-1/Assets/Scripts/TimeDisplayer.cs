using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeDisplayer : MonoBehaviour {

    private TextMeshProUGUI timer;
    private float startTime;

    private void Awake() {
        timer = GetComponent<TextMeshProUGUI>();
        startTime = Time.time;
    }

    private void Update() {
        int timePassed = (int) Mathf.Floor(Time.time - startTime);
        processTime(timePassed, out int minutes, out int seconds);
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
