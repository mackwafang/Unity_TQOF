using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBehavior : MonoBehaviour {
	//public int attack = 0; //Actual attack value
	public float dmg_mod = 1; //Damage modifier in multiplier
	private float aliveTime = 0;
	public int maxAliveTime = 5; //5 seconds
	public bool isCritical = false;

	/* Piercing hits */
	public bool isPierce = false;
	public int maxPierce = 0;
	public List<int> pierceList = null;

	/* Multihit */
	public bool isMultiHit = false;
	public int hit = 0;
	public int maxHit = 0;
	public float hitInterval = 0.99f;
	public int maxHitInterval = 0;


	// Update is called once per frame
	void Update () {
		if (aliveTime < maxAliveTime) {
			aliveTime += Time.deltaTime;
		} else {
			Destroy (gameObject);
		}
	}

	public void setPiercing(int maxPierce) {
		isPierce = true;
		this.maxPierce = maxPierce+1;
		pierceList = new List<int>(maxPierce);
	}
	public void setMultihit(int maxHit, int hitInterval) {
		isMultiHit = true;
		this.maxHit = maxHit;
		maxHitInterval = hitInterval;
	}
	public GameObject enemyNearest() {
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject nearest = null;
		float nearestDist = 0.0f;
		if (enemyList.Length > 0) {
			foreach (GameObject e in enemyList) {
				Vector3 from = transform.position;
				Vector3 to = e.transform.position;
				float dist = Vector3.Distance(from,to);
				if (dist < nearestDist) {
					nearestDist = dist;
					nearest = e;
				}
			}
			return nearest;
		}
		else {
			return null;
		}
	}
}
