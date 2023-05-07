using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private GameObject miniBoss;
    private Vacuum vacuum;

    public GameObject powerupPrefab;
    private float spawnRange = 9f;
    private int enemyCount;
    private int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        vacuum = miniBoss.GetComponent<Vacuum>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0 && !vacuum.isCurrentlyActive) {
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
            waveNumber++;
        }
    }

    private Vector3 GenerateSpawnPosition() {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    private void SpawnEnemyWave(int enemiesToSpawn) {
        if (enemiesToSpawn % 4 == 0) {
            StartCoroutine("AwakeVacuum");
            return;
        }

        for (int i = 0; i < enemiesToSpawn; i++) {
            int randomIndex = Random.Range(0,2);
            Instantiate(enemyPrefab[randomIndex], GenerateSpawnPosition(), enemyPrefab[randomIndex].transform.rotation);
        }
    }


    private IEnumerator AwakeVacuum() {
        vacuum.isCurrentlyActive = true;
        miniBoss.transform.rotation = Quaternion.Euler(0, Random.Range(0, 361), 0);
        vacuum._animator.SetTrigger("Awake");
        yield return new WaitForSeconds(5);
        vacuum._animator.SetTrigger("GoSleep");
        vacuum.isCurrentlyActive = false;
    }
}
