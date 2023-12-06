using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyController : MonoBehaviour {
    private GameObject player;
    private BulletController bulletShooter;
    [SerializeField] private float rotateSpeed = 5f;
    private float maximumHealth = 20f;

    private void Awake() {
        player = GameObject.Find("/Player");
        bulletShooter = GetComponent<BulletController>();
        StartCoroutine(bulletShooter.ShootPlayer(player.transform, transform));
    }

    private void Update() {
        LookAtPlayer();
    }

    private void LookAtPlayer () {
        int dir_rotation = -90;
        if (transform.position.x - player.transform.position.x > 0) {
            dir_rotation = +90;
        }

        Quaternion lookAtRotation = Quaternion.Euler(0, 0, Mathf.Atan((player.transform.position.y - transform.position.y) / (player.transform.position.x - transform.position.x))*(180/Mathf.PI) + dir_rotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * rotateSpeed);
    }

    public float GetMaxHealth () {
        return maximumHealth;
    }
}
