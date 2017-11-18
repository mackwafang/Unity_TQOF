using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehavior : MonoBehaviour {
	public int spawnLimit = 15;
	public int spawnInverval = 120; //frames
	public GameObject [] enemyList = new GameObject[10];
	public int time = 0;
	
	// Update is called once per frame
	void Update () {
		int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
		if (enemyCount < spawnLimit) {
			if (time < spawnInverval) {
				time++;
			}
			else {
				time = 0;
				int index = 0;//Mathf.RoundToInt(Random.Range(0,10));
				if (enemyList[index] != null) {
					GameObject toSpawn = Instantiate(enemyList[index]);
					GameObject player = GameObject.Find("Player");
					toSpawn.transform.position = player.transform.position+(new Vector3(Random.Range(1,10),Random.Range(1,10),0));
					
					EnemyBehavior enemy = toSpawn.GetComponent<EnemyBehavior>();
					PlayerBehavior p = player.GetComponent<PlayerBehavior>();
					enemy.level = p.level;
					enemy.maxHP = enemy.level*50;
				}
			}
		}
	}
}
