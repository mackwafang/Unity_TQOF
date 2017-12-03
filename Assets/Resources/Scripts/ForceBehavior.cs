using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBehavior : MonoBehaviour {
	//public int attack = 0; //Actual attack value
	public float dmg_mod = 1; //Damage modifier in multiplier
	private float aliveTime = 0;
	public int maxAliveTime = 5; //5 seconds
	public bool isCritical = false;

	/* Homing */
	private bool isHoming = false;
	private GameObject homingTarget = null;
	private float homeDelay = 0;
	private float delayTimer = 0;

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

	private Rigidbody2D body;

	void Start() {
		body = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if (isHoming) {
			if (homeDelay > 0) {
				if (delayTimer >= homeDelay) {
					homingTarget = enemyNearest();
					if (homingTarget != null) {
						homingTarget = enemyNearest();
						transform.Translate(new Vector2(body.velocity.magnitude/20,0));
						float xx = homingTarget.transform.position.x - transform.position.x;
						float yy = homingTarget.transform.position.y - transform.position.y;
						float targetRot = Mathf.Atan2 (yy, xx) * Mathf.Rad2Deg;//Mathf.Sqrt (Mathf.Pow (transform.position.x+screenPos.x,2) + Mathf.Pow(transform.position.y+screenPos.y,2));
						transform.rotation = Quaternion.Euler(0,0,targetRot);
					}
				}
				else {
					delayTimer += Time.deltaTime;
				}
			}
		}
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

	public void setHoming (bool isHoming, float delay) {
		this.isHoming = isHoming;
		homeDelay = delay;
	}
	public GameObject enemyNearest() {
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject nearest = null;
		float nearestDist = 10000;
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
