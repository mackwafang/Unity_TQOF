using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill {
	private string name = "";
	private string description = "";
	private int skillLevel = 0;
	private int skillMaxLevel = 0;
	private int requiredLevel = 0;
	private string [] requiredSkills = null;
	private float primaryEffect = 0;
	private float secondaryEffect = 0;
	private float tertiaryEffect = 0;
	private float effectIncrease = 0;
	private int mpCost = 0;
	private bool active = false;
	private int cooldown = 0;
	private Sprite icon = null; //icon use for UI;

	public Skill (string name, string description, int maxLevel, int requiredLevel, string[] requiredSkills,
		float primaryEffect, float secondaryEffect, float tertiaryEffect, float effectIncrease, int mpCost,
		int cooldown, bool isActive) {
		
		this.name = name;
		this.description = description;
		skillMaxLevel = maxLevel;
		this.requiredLevel = requiredLevel;
		this.requiredSkills = requiredSkills;
		this.primaryEffect = primaryEffect;
		this.secondaryEffect = secondaryEffect;
		this.tertiaryEffect = tertiaryEffect;
		this.effectIncrease = effectIncrease;
		this.mpCost = mpCost;
		this.cooldown = cooldown;
		active = isActive;
	}
	public string getName() {
		return name;
	}
	public int getSkillLevel() {
		return skillLevel;
	}
	public int getSkillMaxLevel() {
		return skillMaxLevel;
	}
	public int getRequiredLevel() {
		return requiredLevel;
	}
	public string getDescription() {
		return description;
	}
	public float getPrimaryEffect() {
		return primaryEffect;
	}
	public float getSecondaryEffect() {
		return secondaryEffect;
	}
	public float getTertiaryEffect() {
		return tertiaryEffect;
	}
	public float getEffectIncrease() {
		return effectIncrease;
	}
	public string[] getRequiredSkills() {
		return requiredSkills;
	}
	public Sprite getIcon() {
		return icon;
	}
	public bool isSkillActive() {
		/*Is current skill an active skill?*/
		return active;
	}
	public bool isSkillActive(Skill s) {
		/* is skill s an active?*/
		return s.active;
	}
	public int getMPCost() {
		return mpCost;
	}
	public int getCooldown() {
		return cooldown;
	}

	public void setIcon(Sprite i) {
		icon = i;
	}
	public void setMPCost(int cost) {
		mpCost = cost;
	}
	public void setRequiredLevel(int req) {
		requiredLevel = req;
	}
	public void setCooldown(int value) {
		cooldown = value;
	}
}
