using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float playerSpeed = 5f;
    private bool rewindTime = false;
    private List<Vector3> movements = new List<Vector3>();
    private BulletController bulletShooter;

    private void Awake() {
        bulletShooter = GetComponent<BulletController>();
    }

    private void Update() {
        if (rewindTime) {
            RewindTime();
        } else {
            PlayerMovement();
            RecordMovements();

            if (Input.GetKeyDown(KeyCode.Space)) {
                bulletShooter.ShootGun(transform.GetChild(1).gameObject);
            }
        }
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
        int manipulationFactor = 1;

        if (Input.GetKey(KeyCode.Backspace)) {
            manipulationFactor = 2;
            rewindTime = false;
            
            if (Input.GetKey(KeyCode.Return)) {
                manipulationFactor = -1;
                rewindTime = true;
            }
        }

        return manipulationFactor;
    }

    private void RecordMovements() {
        // Limit the number of positions that the list stores
        movements.Add(transform.position);
    }

    private void RewindTime() {
        if (movements.Count() != 0) {
            transform.position = movements[movements.Count() - 1];
            movements.RemoveAt(movements.Count() - 1);
        }
    }

}