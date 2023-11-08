using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int GetTimeManipulation() {
        if (Input.GetKey(KeyCode.Space)) {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                return 4;
            }
            return 2;
        }
        return 1;
    }

}