using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject enemyPrefab;
	private float spawnRange = 9;
	public int enemyCount;
	public int waveCount = 1;
	public GameObject powerupPrefab;

    // Start is called before the first frame update
    void Start()
    {
		Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
		SpawnEnemyWave(waveCount);
    }

    // Update is called once per frame
    void Update()
    {
		enemyCount = FindObjectsOfType<Enemy>().Length;
		if (enemyCount == 0){
			Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
			SpawnEnemyWave(waveCount);
			waveCount = waveCount + 1;
		}
    }

    private Vector3 GenerateSpawnPosition(){
		float spawnPosX = Random.Range(-spawnRange, spawnRange);
		float spawnPosZ = Random.Range(-spawnRange, spawnRange);
		Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
		return randomPos;
	}

	private void SpawnEnemyWave(int enemiesToSpawn){
		for (int i = 0; i < enemiesToSpawn; i = i + 1){
			Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
		}
	}
}
