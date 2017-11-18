using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffDisplayBehavior : MonoBehaviour {
	public int timer = 0;
	private Image background = null;
	private Image foreground = null;
	private Text timerText = null;
	
	void Start() {
		timerText = transform.GetChild(2).GetComponent<Text>();
	}

	void Update() {
		timerText.text = timer.ToString();
	}
}
