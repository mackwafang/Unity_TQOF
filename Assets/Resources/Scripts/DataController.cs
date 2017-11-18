using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour {
	public KeyCode [] skillKeys = new KeyCode[4];
	public KeyCode [] itemKeys = new KeyCode[4];

	public Skill[] hotkeySkills = new Skill[4];
	public Skill[] skill;
	public float[] skillCooldown = new float[4];

	public GameObject[] ui_skill = new GameObject[4];
	private Slider[] skill_cd_bar = new Slider[4]; // only for display
	private GameObject[] skill_usable_sprite = new GameObject[4];
	private Text[] skill_cd_text = new Text[4]; // only for display

	// Use this for initialization
	void Start () {
		DataGatherer gatherer = new DataGatherer();
		skill = new Skill[gatherer.getData().Length];
		skill = gatherer.getData();
		setSkill(0,"Heavy Impact");
		setSkill(1,"Piercing Force");
		setSkill(2,"Swarm");
		setSkill(3,"Escort");
		skillCooldown[0] = hotkeySkills[0].getCooldown();
		skillCooldown[1] = hotkeySkills[1].getCooldown();
		skillCooldown[2] = hotkeySkills[2].getCooldown();
		skillCooldown[3] = hotkeySkills[3].getCooldown();

		for (int i = 0; i < 4; i++) {
			if (ui_skill != null) {
				skill_cd_bar[i] = ui_skill[i].GetComponent<Slider>();
				GameObject c = ui_skill[i].transform.GetChild(3).gameObject;
				skill_cd_text[i] = c.GetComponent<Text>();
				c = ui_skill[i].transform.GetChild(2).gameObject;
				skill_usable_sprite[i] = c;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 4; i++) {
			if (ui_skill[i] != null) {
				if (hotkeySkills[i] != null) {
					/* Black out if player does not have the proper requirement */
					PlayerBehavior player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
					if (player != null) {
						if (player.level < hotkeySkills[i].getRequiredLevel() && player.mp < hotkeySkills[i].getMPCost()) {
							//skill is unusable
							skill_usable_sprite[i].SetActive(true);
						}
						else {
							//skill is usable
							if (skillCooldown[i] <= 0 && skill_usable_sprite[i].activeSelf) {
								createSkillNotice(ui_skill[i]);
							}
							skill_usable_sprite[i].SetActive(false);
						}
					}
					/* Set display values */
					if (skill_cd_bar[i] != null) {
						skill_cd_bar[i].maxValue = hotkeySkills[i].getCooldown();
						skill_cd_bar[i].value = skillCooldown[i];
					}
					/* skill cooldown */
					if (skillCooldown[i] > 0) {
						skillCooldown[i]--;
						if (skillCooldown[i] == 1) {
							createSkillNotice(ui_skill[i]);
						}
						if ((skillCooldown[i] / 60) > 10) {
							skill_cd_text[i].text = Mathf.RoundToInt(skillCooldown[i]/60).ToString();
						}
						else {
							skill_cd_text[i].text = (skillCooldown[i]/60).ToString("F1");
						}
					}
					else {
						skill_cd_text[i].text = "";
					}
				}
			}
		}
	}

	public bool setSkill(int slot, string skillName) {
		bool skillSet = false;
		foreach(Skill s in skill) {
			if (skillName.Equals(s.getName())) {
				hotkeySkills[slot] = s;
				skillSet = true;
				break;
			}
		}
		return skillSet;
	}

	private void createSkillNotice(GameObject skill) { 
		GameObject notice_r = Resources.Load<GameObject>("GameObjects/SkillNotice");
		if (notice_r != null) {
			GameObject notice = Instantiate(notice_r);
			notice.transform.SetParent(skill.transform);
			notice.transform.position = skill.transform.position;
		}
	}
}
