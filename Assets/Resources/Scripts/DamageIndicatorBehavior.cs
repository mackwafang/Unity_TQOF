using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorBehavior : MonoBehaviour {
	private Animator a;
	private Text text;
	public int damage;
	public int trigger = 0;
	private Vector3 original = new Vector3();
	// Use this for initialization
	void Start () {
		a = GetComponent<Animator>();
		text = GetComponent<Text>();
		text.text = damage.ToString();
		switch(trigger) {
			case 1:
				a.SetBool("isCritical",true);
				break;
			case 2:
				a.SetBool("isHealing",true);
				break;
			default:
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		a.speed = 1+Time.deltaTime;
		if (a.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
			Destroy(gameObject);
		}
	}
}
