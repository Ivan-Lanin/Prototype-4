using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject powerupIndicator;
    private GameObject focalPoint;
    private bool hasPowerup = false;
    private float powerupStrength = 15f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }


    void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        if (forwardInput != 0) {
            playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        }
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Powerup") && !hasPowerup) {
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup) {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayerDirection = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with " + collision.gameObject.name + "with powerup set to " + hasPowerup);
            enemyRb.AddForce(awayFromPlayerDirection * powerupStrength, ForceMode.Impulse);
        }
    }

    private IEnumerator PowerupCountdownRoutine() {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
}
