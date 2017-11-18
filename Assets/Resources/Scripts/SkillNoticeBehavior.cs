using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNoticeBehavior : MonoBehaviour {
	// Update is called once per frame
	private Animator a;

	void Start() {
		a = GetComponent<Animator>();
	}
	
	void Update () {
		if (a.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
			Destroy(gameObject);
		}
	}
}
