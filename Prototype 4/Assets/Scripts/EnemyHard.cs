using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHard : Enemy
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {

            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromHardEnemyDirection = (collision.gameObject.transform.position - transform.position);

            playerRb.AddForce(awayFromHardEnemyDirection * 7f, ForceMode.Impulse);
        }
    }
}
