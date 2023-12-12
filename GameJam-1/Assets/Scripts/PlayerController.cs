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

        Vector3 raycastPosition = transform.position;
        Vector3 raycastScale = transform.localScale;
        float raycastAngle = Mathf.Atan(moveDir.y/moveDir.x) * (180/Mathf.PI);
        // float raycastOffest = (float) 0.05;
        // if (moveDir.x == 0) {
        //     raycastPosition = transform.position + new Vector3(0, transform.localScale.y + raycastOffest, 0);
        //     raycastScale = new Vector3(1, raycastOffest, 0);
        // } else if (moveDir.y == 0) {
        //     raycastPosition = transform.position + new Vector3(transform.localScale.x + raycastOffest, 0, 0);
        //     raycastScale = new Vector3(raycastOffest, 1, 0);
        // } else {
        //     raycastPosition = transform.position + new Vector3(transform.localScale.x + raycastOffest, transform.localScale.y + raycastOffest, 0).normalized;
        //     raycastScale = new Vector3(raycastOffest, 1, 0);
        // }

        float moveDistance = Time.deltaTime * playerSpeed;

        // bool canMove = !Physics2D.BoxCast(transform.position, transform.localScale, Mathf.Atan(moveDir.y/moveDir.x) * (180/Mathf.PI), moveDir, moveDistance);
        RaycastHit2D[] characterHit = Physics2D.BoxCastAll(raycastPosition, raycastScale, raycastAngle, moveDir, moveDistance);
        // string debugOutput = "";
        GameObject character = null;
        if (characterHit.Length >= 1) {
            // foreach(RaycastHit2D castHit in characterHit) {
            //     debugOutput += castHit.collider.name + ", ";
            // }
            character = characterHit[0].collider.gameObject;
            if (characterHit[0].collider.CompareTag("Player")){
                if (characterHit.Length >= 2) {
                    character = characterHit[1].collider.gameObject;
                } else {
                    character = null;
                }
            }

            // Debug.Log(debugOutput);
        }

        if (character != null) {
            if (character.CompareTag("Wall")) {
                Vector2 moveDirX = new Vector2(moveDir.x, 0);
                Vector2 moveDirY = new Vector2(0, moveDir.y);

                float raycastAngleX = 0;
                float raycastAngleY = 90;

                RaycastHit2D[] characterHitX = Physics2D.BoxCastAll(raycastPosition, raycastScale, raycastAngleX, moveDirX, moveDistance);
                RaycastHit2D[] characterHitY = Physics2D.BoxCastAll(raycastPosition, raycastScale, raycastAngleY, moveDirY, moveDistance);
                // RaycastHit2D characterHitY = Physics2D.BoxCast(transform.position, transform.localScale, Mathf.Atan(moveDir.y/moveDir.x) * (180/Mathf.PI), moveDirY, moveDistance);

                // GetComponent<RayCastVisualiser>().showRaycast(raycastPosition + new Vector3(moveDir.x, 0, 0).normalized, raycastScale);
                // GetComponent<RayCastVisualiser>().showRaycast(raycastPosition + new Vector3(0, moveDir.y, 0).normalized, raycastScale);

                GameObject characterX = null;
                if (characterHitX.Length >= 1) {
                    characterX = characterHitX[0].collider.gameObject;
                    if (characterHitX[0].collider.CompareTag("Player")){
                        if (characterHitX.Length >= 2) {
                            characterX = characterHitX[1].collider.gameObject;
                        } else {
                            characterX = null;
                        }
                    }
                }

                GameObject characterY = null;
                if (characterHitY.Length >= 1) {
                    characterY = characterHitY[0].collider.gameObject;
                    if (characterHitY[0].collider.CompareTag("Player")){
                        if (characterHitY.Length >= 2) {
                            characterY = characterHitY[1].collider.gameObject;
                        } else {
                            characterY = null;
                        }
                    }
                }

                Debug.Log(string.Format("X: {0}, Y: {1}", characterX, characterY));

                if (characterX == null) {
                    moveDir = new Vector2(moveDir.x, 0);
                } else {
                    if (!characterX.CompareTag("Wall")) {
                        moveDir = new Vector2(moveDir.x, 0);
                    }
                }
                
                if (characterY == null) {
                    moveDir = new Vector2(0, moveDir.y);
                } else {
                    if (!characterY.CompareTag("Wall")) {
                        moveDir = new Vector2(0, moveDir.y);
                    }
                }

                if (characterX != null && characterY != null) {
                    if (characterY.CompareTag("Wall") && characterY.CompareTag("Wall")) {
                        moveDir = Vector2.zero;
                    }
                }

                if (characterX == null && characterY == null) {
                    moveDir = new Vector2(moveDirX.x, moveDirY.y);
                }
            }
        }

        moveDir = moveDir.normalized;
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

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireCube(transform.position + new Vector3(moveDir.x, 0, 0).normalized, transform.localScale);
    //     Gizmos.DrawWireCube(transform.position + new Vector3(0, moveDir.y, 0).normalized, transform.localScale);
    // }
}