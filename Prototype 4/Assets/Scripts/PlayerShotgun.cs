using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotgun : MonoBehaviour
{
    private GameObject[] enemies;
    [SerializeField] private GameObject projectilePrefab;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ShootAtEnemies();
        }
    }


    private void ShootAtEnemies() {
        FindEnemies();
        foreach (GameObject enemy in enemies) {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().FlyTowards((enemy.transform.position - transform.position).normalized);
        }
    }


    private void FindEnemies() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
