using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour {
	public string name = "";
	public bool canMove = true;
	public int level = 1;
	public int xp = 0;
	public int maxXP = 10;
	public int xpGain = 0;
	public int moneyDrop = 0;
	//Item drop here
	

	public int maxHP = 0;
	public int maxMP = 0;
	public int hp = 1;
	public float hpDisplay = 0;
	public float mpDisplay = 0;
	public int mp = 1;
	public int hp_regen = 1; //gives 1 hp/s
	public int mp_regen = 1; //gives 1 mp/s
	public float hp_regen_time = 0; //updates timer
	public float mp_regen_time = 0; //updates timer
	[Range(0,1)] protected float base_hp_regen_interval = 0.5f;
	[Range(0,1)] protected  float base_mp_regen_interval = 0.5f;
	public float hp_regen_interval = 0; //updates every second
	public float mp_regen_interval = 0; //updates every second

	public int str = 0;
	public int stam = 0;
	public int mana = 0;
	public int luck = 0;
	public float speed = 1;
	public float critical = 0;
	public int criticalDamage = 0;

	private GameObject p;
	private PlayerBehavior player;
	private float updateTime = 0;
	public List<Buff> buffList = new List<Buff>();


	public void updateStats() {
		/* XP and Level UP */ 
		if (xp >= maxXP) {
			xp -= maxXP;
			maxXP = Mathf.RoundToInt(maxXP*1.5f); //or some other formula here
			level ++;
			p = GameObject.Find("Player");
			player = p.GetComponent<PlayerBehavior>();
			if (p != null) {
				/* Player level up */
				player.str += 5;
				player.stam += 5;
				player.mana += 5;
				player.luck += 5;
				player.maxHP += 20;
				player.maxMP += 30;
				if (player.level % 3 == 0) {
					player.hp_regen += 1;
					player.mp_regen += 1;
					player.hp_regen_interval *= 0.9f;
					player.mp_regen_interval *= 0.9f;
				}
				player.criticalDamage = Mathf.RoundToInt(criticalDamage * 1.05f);
			}
		}
		/* Dies */
		if (hp <= 0) {
			if (player != null) {
				player.xp += xpGain;
			}
			Destroy (gameObject);
		}
		if (updateTime >= 1) {
			if (canMove) {
				/*Reduce timer for buffs*/
				foreach (Buff b in buffList) {
					if (b != null) {
						b.reduceTime ();
					}
				}
			}
			updateTime = 0;
		} else {
			updateTime += Time.deltaTime;
		}
		List<Buff> toRemove = new List<Buff>(); // to avoid InvalidOperatorException
		foreach(Buff b in buffList) {
			if (b != null && b.getTime() < 0) {
				toRemove.Add(b);
			}
		}
		foreach(Buff b in toRemove) {
			buffList.Remove(b);
		}
		/* hp/mp regeneration*/
		if (hp < maxHP) {
			if (hp_regen_time < hp_regen_interval+base_hp_regen_interval) {
				hp_regen_time += Time.deltaTime;
			} else {
				hp_regen_time = 0;
				hp += Mathf.Min (hp_regen, maxHP - hp);
			}
		}
		if (mp < maxMP) {
			if (mp_regen_time < mp_regen_interval+base_mp_regen_interval) {
				mp_regen_time += Time.deltaTime;
			} else {
				mp_regen_time = 0;
				mp += Mathf.Min (mp_regen, maxMP - mp);
			}
		}
		/* HP/MP Smooth transition */
		if (hp > hpDisplay) {
			hpDisplay = Mathf.MoveTowards (hpDisplay, hp, maxHP*0.005f);
		} else {
			hpDisplay = hp;
		}
		if (mp > mpDisplay) {
			mpDisplay = Mathf.MoveTowards (mpDisplay, mp, maxMP*0.005f);
		} else {
			mpDisplay = mp;
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (gameObject.tag.Equals("Player")) {
			if (other.gameObject.tag.Equals("Enemy")) {
				Rigidbody2D p_body = GetComponent<Rigidbody2D>();
				p_body.AddRelativeForce(new Vector2(1,1),ForceMode2D.Force);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		GameObject o = other.gameObject;
		/* Player projectile hit enemy */
		if (o.CompareTag("Player Force") && gameObject.CompareTag("Enemy")) {
			ForceBehavior force = o.GetComponent<ForceBehavior>();
			if (force != null) {
				/* deals damage */
				//int damage = Mathf.RoundToInt(force.attack*force.dmg_mod);
				int damage = 0;
				bool c = false;
				p = GameObject.Find("Player");
				player = p.GetComponent<PlayerBehavior>();
				if (p != null) {
					damage = Mathf.RoundToInt(player.mana*force.dmg_mod);
					if (Random.Range(0,100) <= player.critical) {
						damage += player.criticalDamage;
						c = true;
					}
				}
				else {
					//If player does not exists, do minimal damage
					damage = 1;
				}
				if (force.isPierce) {
					/* Piercing */
					bool inList = false;
					if (force.pierceList.Count < force.maxPierce) {
						foreach (int id in force.pierceList) {
							if (id == GetInstanceID()) {
								inList = true;
								break;
							}
						}
						if (!inList) {
							force.pierceList.Add(GetInstanceID());
							hp -= damage;
							createIndicator(damage, c ? 1 : 0);
							if (force.pierceList.Count == force.maxPierce) {
								Destroy(o);
							}
						}
					}
				}
				else if (force.isMultiHit) {
					/* Multihit */
					if (force.hitInterval < force.maxHitInterval) {
						/* Set interval */
						force.hitInterval++;
					}
					else {
						force.hitInterval = 0;
						/* Do a hit */
						if (force.hit < force.maxHit) {
							force.hit ++;
						}
						hp -= damage;
						createIndicator(damage,c ? 1 : 0);
					}
				}
				else {
					/* normal */
					hp -= damage;
					createIndicator(damage,c ? 1 : 0);
					Destroy(o); //Destroy the colliding object
				}
			}
			else {
				Debug.Log("force does not exists");
			}
		}
	}

	public Buff addBuff(string name, string desc, int time, float primary, float secondary, bool perm) {
		Buff b = new Buff(name,desc,time,primary,secondary,perm);
		buffList.Add(b);
		return b;
	}

	public bool isBuffActive(string name) {
		foreach(Buff b in buffList) {
			if (b.getName().Equals(name)) {
				return true;
			}
		}
		return false;
	}

	public Buff findBuff(string name) {
		foreach(Buff b in buffList) {
			if (b.getName().Equals(name)) {
				return b;
			}
		}
		return null;
	}
	
	private void createIndicator(int value, int trigger) {
		/* Create Popup */
		GameObject indicatorResource = Resources.Load<GameObject>("GameObjects/DamageIndicator");
		if (indicatorResource != null) {
			GameObject indicator = Instantiate(indicatorResource);
			indicator.transform.position = new Vector3(transform.position.x,transform.position.y+1,0);
			DamageIndicatorBehavior indicatorBehavior = indicator.GetComponent<DamageIndicatorBehavior>();
			indicatorBehavior.damage = value;
			indicatorBehavior.trigger = trigger;
		}
		else {
			Debug.Log("indicator is null");
		}
	}
}
