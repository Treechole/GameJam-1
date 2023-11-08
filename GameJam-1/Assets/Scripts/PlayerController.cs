using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // private Transform position;

    // private void Awake() {
    //     position = GetComponent<Transform>();
    // }

    [SerializeField] private float playerSpeed = 5f;

    private void Update() {
        PlayerMovement();
    }

    private void PlayerMovement() {
        Vector2 position = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            position.y += 1;
        }

        if (Input.GetKey(KeyCode.S)) {
            position.y -= 1;
        }

        if (Input.GetKey(KeyCode.D)) {
            position.x += 1;
        }

        if (Input.GetKey(KeyCode.A)) {
            position.x -= 1;
        }

        position = position.normalized;
        transform.position += new Vector3(position.x, position.y ,0) * Time.deltaTime * playerSpeed;
    }

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