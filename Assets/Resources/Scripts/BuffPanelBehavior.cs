using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPanelBehavior : MonoBehaviour {
	private List<Buff> playerBuffList = null;
	private List<GameObject> buffDisplay = new List<GameObject>();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			buffDisplay.Add(Instantiate(Resources.Load<GameObject>("GameObjects/BuffDisplay")));
			buffDisplay[i].transform.SetParent(gameObject.transform);
			buffDisplay[i].transform.position = gameObject.transform.position;
			buffDisplay[i].transform.position += (new Vector3(16+(i*66),-16,0));
			buffDisplay[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
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
	}
}
