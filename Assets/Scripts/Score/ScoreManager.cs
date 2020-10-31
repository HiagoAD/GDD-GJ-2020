using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	[SerializeField] private ScoreDataObject scoreData = null;

	void Start () {
		this.scoreData.ResetScoreData ();
	}

	public void CountReturnedUnit() {
		this.scoreData.score++;
	}
}