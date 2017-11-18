using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataGatherer {
	string [] stringSkillData = File.ReadAllLines("Assets/Resources/Data/SkillsData.data");
	Skill [] skillData = null;
	public DataGatherer() {
		skillData = new Skill[stringSkillData.Length];
		for (int i = 0; i < skillData.Length; i++) {
			string [] data = stringSkillData[i].Split('|');
			string name = data[0];
			string description = data[1];
			int maxLevel = int.Parse(data[2]);
			int requiredLevel = int.Parse(data[3]);
			string[] requiredSkills;
			if (data[4][0] == '[') {
				/* Indicate mulitple skills */
				string s = null;
				for (int j = 1; j < data[4].Length-2; j++) {
					s.Insert(j,data[4][j].ToString());
				}
				requiredSkills = new string[s.Length];
				requiredSkills = s.Split(',');
			}
			else {
				/* Single skill */
				requiredSkills = new string[1];
				requiredSkills[0] = (data[4].Equals("null") ? null : data[4]);
			}
			float p_e = float.Parse(data[5]);
			float s_e = float.Parse(data[6]);
			float t_e = float.Parse(data[7]);
			float i_e = float.Parse(data[8]);
			int cost = int.Parse(data[9]);
			int cd = int.Parse(data[10]);
			bool a;
			if (data[11].Equals("TRUE")) {
				a = true;
			}
			else if (data[11].Equals("FALSE")) {
				a = false;
			}
			else {
				a = false;
			}

			skillData[i] = new Skill(name,description,maxLevel,requiredLevel,requiredSkills,p_e,s_e,t_e,i_e,cost,cd,a);

		}
	}
	public Skill [] getData() {
		return skillData;
	}
}
