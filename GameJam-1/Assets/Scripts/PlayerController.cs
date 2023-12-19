using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float playerSpeed = 5f;
    private bool rewindTime = false;
    private List<Vector3> movements = new List<Vector3>();
    private BulletController bulletShooter;
    private float maximumHealth = 100f;
    private bool hasGun = false;
    private Vector2 moveDir = Vector2.zero;

    // Update the system to have gun shooter instead of bullet shooter - replace scripts from BulletController.cs to GunController.cs

    private void Awake() {
        bulletShooter = GetComponent<BulletController>();
    }

    private void Update() {
        if (rewindTime) {
            RewindTime();
        } else {
            PlayerMovement();
            RecordMovements();

            if (Input.GetKeyDown(KeyCode.Mouse0) && hasGun) {
                GameObject gun = FindWithTag(gameObject, "Gun");
                if (gun.GetComponent<AmmoController>().checkAmmo()) {
                    bulletShooter.ShootGun(gun);
                }
            }
        }
    }

    public Vector2 MovementInputNormalized() {
        moveDir = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            moveDir.y += 1;
        }

        if (Input.GetKey(KeyCode.S)) {
            moveDir.y -= 1;
        }

        if (Input.GetKey(KeyCode.D)) {
            moveDir.x += 1;
        }

        if (Input.GetKey(KeyCode.A)) {
            moveDir.x -= 1;
        }

        return moveDir.normalized;
    }

    private void PlayerMovement() {
        Vector2 moveDir = MovementInputNormalized();
        float moveDistance = Time.deltaTime * playerSpeed;

        moveDir = ChangeMovementForWall(moveDir);
        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * moveDistance;
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

    public void SetGun() {
        hasGun = true;
    }

    public bool CheckGun() {
        return hasGun;
    }

    public GameObject FindWithTag (GameObject parent, string tag) {
        GameObject requiredChild = null;

        foreach (Transform transform in parent.transform) {
            if (transform.CompareTag(tag)) {
                requiredChild = transform.gameObject;
                break;
            }
        }

        return requiredChild;
    }

    public float GetMaxHealth () {
        return maximumHealth;
    }

    private bool IsCollidingWithWall(RaycastHit2D[] hits2D) {
        bool isColliding = false;

        foreach(RaycastHit2D hit2D in hits2D) {
            if (hit2D.collider.CompareTag("Wall")) {
                isColliding = true;
                break; 
            }
        }

        return isColliding;
    }

    private Vector2 ChangeMovementForWall(Vector2 moveDir) {
        Vector3 raycastPosition = transform.position;
        Vector3 raycastScale = transform.localScale;

        Vector2 moveDirX = new Vector2(moveDir.x, 0);
        float raycastAngleX = 0;

        Vector2 moveDirY = new Vector2(0, moveDir.y);
        float raycastAngleY = 90;

        float moveDistance = Time.deltaTime * playerSpeed;

        RaycastHit2D[] characterHitsX = Physics2D.BoxCastAll(raycastPosition, raycastScale, raycastAngleX, moveDirX, moveDistance);
        RaycastHit2D[] characterHitsY = Physics2D.BoxCastAll(raycastPosition, raycastScale, raycastAngleY, moveDirY, moveDistance);

        bool wallAtX = IsCollidingWithWall(characterHitsX);
        bool wallAtY = IsCollidingWithWall(characterHitsY);

        if (wallAtX && wallAtY) {
            return Vector2.zero;
        } else if (wallAtX && !wallAtY) {
            return moveDirY.normalized;
        } else if (!wallAtX && wallAtY) {
            return moveDirX.normalized;
        } else {
            return moveDir;
        }
    }

}