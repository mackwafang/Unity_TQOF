using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : LivingEntity {
	private GameObject canvas;
	private GameObject healthBar;
	private GameObject healthBarResource;
	private Rigidbody2D body;

	void Start() {
		hp = maxHP;
		mp = maxMP;
		hpDisplay = maxHP;
		mpDisplay = maxMP;
		
		body = GetComponent<Rigidbody2D>();
		canvas = GameObject.Find("UI");
		healthBarResource = Resources.Load<GameObject>("GameObjects/EnemyHealthBar");
	}
	void Update () {
		GameObject player = GameObject.Find("Player");
		/* Facing/moving towards player */
		if (canMove) {
			LookAt2D(new Vector2(player.transform.position.x,player.transform.position.y));
			transform.position = Vector3.MoveTowards(transform.position,player.transform.position,speed*0.05f);
		}

		if (healthBar == null) {
			//health bar does not exists
			healthBar = Instantiate(healthBarResource);
		}
		else {
			//health bar now exists
			healthBar.transform.SetParent(canvas.transform);
			healthBar.transform.SetAsFirstSibling();
			healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + (new Vector3(-0.5f,0.5f,0)));
			Slider health = healthBar.GetComponent<Slider>();
			health.maxValue = maxHP;
			health.minValue = 0;
			health.value = hpDisplay;
			Text levelText = healthBar.transform.GetChild(2).GetComponent<Text>();
			levelText.text = level.ToString();
		}
		if (hp <= 0) {
			Destroy(healthBar);
		}
		updateStats ();
	}

	private void LookAt2D(Vector2 to) {
		/*
			Rotate the gameObject to face "to" vector
		 */
		Vector2 from = new Vector2(transform.position.x,transform.position.y);
		float xx = to.x-transform.position.x;
		float yy = to.y-transform.position.y;

		float angle = Mathf.Atan2(yy,xx)*Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0,0,angle);
	}
}
