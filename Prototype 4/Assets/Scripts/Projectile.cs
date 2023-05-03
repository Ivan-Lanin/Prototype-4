using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody projectileRb;
    [SerializeField] private float speed = 40f;
    Vector3 enemyDirection = Vector3.zero;


    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        projectileRb.AddForce(enemyDirection * speed, ForceMode.Acceleration);
    }

    public void FlyTowards(Vector3 direction) {
        enemyDirection = direction;
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromHardEnemyDirection = (other.gameObject.transform.position - transform.position).normalized;

            enemyRb.AddForce(awayFromHardEnemyDirection * 7f, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
