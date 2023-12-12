using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RayCastVisualiser : MonoBehaviour {
    private Vector3 toPosition;
    private PlayerController player;
    private Vector2 moveDir = Vector2.zero;
    public float castAngle = 0;
    public float raycastDistance = 1f;
    public float scaleMultiplier = (float) 1;

    private Vector3 initPosition;
    private Vector3 initScale;
    // private float initAngle = 0;
    // private Vector2 initDirection;
    // private float initDistance;

    private void Awake() {
        player = GetComponent<PlayerController>();

        initPosition = transform.position;
        initScale = transform.localScale * scaleMultiplier;
        // initAngle = castAngle;
        // initDirection = moveDir;
        // initDistance = raycastDistance;
    }

    // private void Update() {
    //     Vector2 tempDir = player.MovementInputNormalized();
    //     if (tempDir != Vector2.zero) {
    //         moveDir = tempDir;
    //     }

    //     RaycastHit2D[] hit2D = Physics2D.BoxCastAll(transform.position, transform.localScale * scaleMultiplier, castAngle, moveDir, raycastDistance);
    //     string output = "";
    //     foreach(RaycastHit2D raycastHit in hit2D) {
    //         output += raycastHit.collider.name;
    //         output += " ";
    //     }
    //     Debug.Log(output);
    //     toPosition = transform.position + new Vector3(moveDir.x, moveDir.y, 0) * raycastDistance;

    //     // if (hit2D.Length >= 1) {
    //     //     Debug.Log(string.Format(">>> {0}", hit2D[0].name));
    //     // }
    //     // if (hit2D.collider != null) {
    //     //     Debug.Log(string.Format(">>> {0}", hit2D.collider.name));
    //     // }
    // }

    public void showRaycast(Vector3 position, Vector3 scale) { //}, float angle, Vector2 direction, float distance) {
        initPosition = position;
        initScale = scale;
        // initAngle = angle;
        // initDirection = direction;
        // initDistance = distance;

        // RaycastHit2D[] hit2D = Physics2D.BoxCastAll(position, scale, angle, direction, distance);
        // toPosition = position + new Vector3(direction.x, direction.y, 0) * distance;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(initPosition, initScale);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(initPosition, toPosition);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(toPosition, initScale);
    }
}
