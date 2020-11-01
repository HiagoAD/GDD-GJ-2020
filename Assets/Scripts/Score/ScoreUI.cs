using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
	private Text text = null;

	void Awake () {
		this.text = GetComponentInChildren<Text> ();
	}

	public void UpdateScore (int score) {
		this.text.text = "" + score;
	}
}