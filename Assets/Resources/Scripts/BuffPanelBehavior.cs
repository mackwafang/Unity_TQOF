using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanelBehavior : MonoBehaviour {
	private List<Buff> playerBuffList = null;
	
	void OnGUI() {
		GUIStyle style = new GUIStyle();
		style.font = Resources.Load<Font>("Font/IMAGINE_FONT");
		style.fontSize = 18;
		style.alignment = TextAnchor.UpperCenter;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.white;
		
		for (int i = 0; i < playerBuffList.Count; i++) {
			Buff b = playerBuffList[i];

			Texture2D buffBackground = Resources.Load<Texture2D>("Sprites/Skill Sprites/background/skillBkg_green");
			Texture2D buffImage = Resources.Load<Texture2D>("Sprites/Skill Sprites/sprite/"+b.getName());
			if (buffBackground != null) {
				int xx = 20+(i*50);
				int yy = 20;
				int w = 48;
				int h = 48;
				GUI.DrawTexture(new Rect(xx,yy,w,h),buffBackground);
				GUI.Label(new Rect(xx,yy+60,w,h),b.getTime().ToString(),style);
				if (buffImage != null) {
					GUI.DrawTexture(new Rect(xx,yy,w,h),buffImage);
				}
			}
		}
	}
	void Update() {
		GameObject player = GameObject.Find("Player");
		if (player != null) {
			playerBuffList = player.GetComponent<PlayerBehavior>().buffList;
		}
	}

	// Update is called once per frame
	// This methods relies on instantiating GameObjects
	/*void Update () {
		GameObject player = GameObject.Find("Player");
		if (player != null) {
			playerBuffList = player.GetComponent<PlayerBehavior>().buffList;
		}
		for (int i = 0; i < playerBuffList.Count; i++) {
			if (playerBuffList[i] != null) {
				if (buffDisplay[i] != null) {
					buffDisplay[i].SetActive(true);
					BuffDisplayBehavior behavior = buffDisplay[i].GetComponent<BuffDisplayBehavior>();
					behavior.timer = playerBuffList[i].getTime();
				}
				else {
					Debug.Log("buffDisplay ["+i+"] is null");
				}
			}
			else {
				buffDisplay[i].SetActive(false);
			}
		}

		int d = 0;
		foreach(GameObject b in buffDisplay) {
			if (b.activeSelf) {d++;}
		}
		int p = playerBuffList.Count;
		if (d-p >= 1) {
			for (int i = d-p-1; i <= d; i++) {
				buffDisplay[i].SetActive(false);
			}
		}
	}*/
}
