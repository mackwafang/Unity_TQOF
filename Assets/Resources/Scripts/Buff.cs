using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff {
	private string name = "";
	private string description = "";
	private int time = 0; //in seconds
	private float primaryEffect = 0;
	private float secondaryEffect = 0;
	private bool isPermanent = false;

	public Buff(string name, string desc, int time, float primary, float secondary, bool perm) {
		this.name = name;
		description = desc;
		this.time = time;
		primaryEffect = primary;
		secondaryEffect = secondary;
		isPermanent = perm;
	}

	public string getName() {
		return name;
	}
	public string getDescription() {
		return description;
	}
	public int getTime() {
		return time;
	}
	public float getPrimaryEffect() {
		return primaryEffect;
	}
	public float getSecondaryEffect() {
		return secondaryEffect;
	}

	public void reduceTime() {
		if (!isPermanent) {
			time -= 1;
		}
	}
}
