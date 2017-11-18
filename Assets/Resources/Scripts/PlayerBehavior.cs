using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : LivingEntity {
	//LivingEntity stats = new LivingEntity("Player",1,10,10,10,10,10,10,5,10);
	private Camera camera;
	public Text hp_text;
	public Slider hp_bar;
	public Text mp_text;
	public Slider mp_bar;
	public Text xp_text;
	public Slider xp_bar;
	public Text level_text;
	public GameObject force;

	private int fire = 0;

	void Start() {
		/*initialize some values*/
		camera = Camera.main;
		level = 1;
		maxHP = 10;
		hp = maxHP;
		maxMP = 30;
		mp = maxMP;
		str = 10;
		stam = 10;
		mana = 10;
		luck = 10;
		critical = 5;
		criticalDamage = 20;
	}

	void Update() {
		Vector2 mouseScreenPos = camera.ScreenToWorldPoint (Input.mousePosition);
		float xx = mouseScreenPos.x - transform.position.x;
		float yy = mouseScreenPos.y - transform.position.y;
		if (fire < 1) {
			fire++;
		}
		if (canMove) {
			camera.transform.position = new Vector3(transform.position.x,transform.position.y,-10);
			/* Movement */
			float hor = Input.GetAxisRaw ("Horizontal")*0.1f;
			float ver = Input.GetAxisRaw ("Vertical")*0.1f;
			if (Mathf.Abs(hor) > 0) {
				transform.position = new Vector2(transform.position.x+speed*hor,transform.position.y);
			}
			if (Mathf.Abs(ver) > 0) {
				transform.position = new Vector2(transform.position.x,transform.position.y+speed*ver);
			}
			float mousePos = Mathf.Atan2 (yy, xx) * Mathf.Rad2Deg;//Mathf.Sqrt (Mathf.Pow (transform.position.x+screenPos.x,2) + Mathf.Pow(transform.position.y+screenPos.y,2));
			transform.rotation = Quaternion.Euler (0,0,mousePos);
			
			if (Mathf.Abs(hor) > 0 || Mathf.Abs(ver) > 0) {
				base_hp_regen_interval = 0.5f;
				base_mp_regen_interval = 0.5f;
			}
			else {
				base_hp_regen_interval = 0.1f;
				base_mp_regen_interval = 0.1f;
			}
 			/*Firing Force*/
			if (Input.GetMouseButton (1)) {
				mp_regen_time = -0.1f;
				if (mp >= 1) {
					if (fire > 0) {
						fire -= 10;
						mp -= 1;
						if (force != null) {
							/* Spawn projectile */
							createForce(new Vector2(xx,yy),10);
						}
					}
				}
			}
			/* Using skills*/
			GameObject controller_object = GameObject.Find("DataControl");
			DataController controller = controller_object.GetComponent<DataController>();
			for (int i = 0; i < 4; i += 1) {
				if (Input.GetKeyDown(controller.skillKeys[i])) {
					if (controller.hotkeySkills[i] != null) {
						if (controller.skillCooldown[i] <= 0) {
							Skill s = controller.hotkeySkills[i];
							if (mp >= s.getMPCost()) {
								mp -= s.getMPCost();
								controller.skillCooldown[i] = s.getCooldown();
								// TODO: USE SKILLS
								if (!isBuffActive(s.getName())) {
									addBuff(s.getName(),s.getDescription(),(s.getCooldown()-120)/60,s.getPrimaryEffect(),s.getSecondaryEffect(),false);
								}
							}
						}
					}
				}
			}
			if (isBuffActive("Swarm")) {
				Buff b = findBuff("Swarm");
				for (int i = 0; i < b.getPrimaryEffect(); i++) {
					//create force
					
				}
			}
			/*Update UI*/
			if (hp_bar != null && hp_text != null) {
				hp_bar.minValue = 0;
				hp_bar.maxValue = maxHP;
				hp_bar.value = hpDisplay;
				hp_text.text = hp.ToString();
			}
			if (mp_bar != null && mp_text != null) {
				mp_bar.minValue = 0;
				mp_bar.maxValue = maxMP;
				mp_bar.value = mpDisplay;
				mp_text.text = mp.ToString();
			}
			if (xp_bar != null && xp_text != null) {
				xp_bar.minValue = 0;
				xp_bar.maxValue = maxXP;
				xp_bar.value = xp;
				xp_text.text = ((xp/maxXP)*100).ToString() + "%";
			}
			if (level_text != null) {
				level_text.text = level.ToString();
			}
		}

		updateStats (); //update stats if any changes were made
	}

	private void createForce(Vector3 to, int speed) {
		GameObject projectile = Instantiate (force, transform.position, transform.rotation);
		ForceBehavior projectileBehavior = projectile.GetComponent<ForceBehavior>();
		Rigidbody2D projectile_body = projectile.GetComponent<Rigidbody2D> ();
		Vector2 norm = new Vector2 (to.x, to.y).normalized;
		projectile_body.velocity = norm * speed;
		if (isBuffActive("Heavy Impact")) {
			Buff b = findBuff("Heavy Impact");
			projectileBehavior.dmg_mod = b.getPrimaryEffect();
		}
		if (isBuffActive("Piercing Force")) {
			Buff b = findBuff("Piercing Force");
			projectileBehavior.setPiercing((int) b.getPrimaryEffect());
		}
	}
}
