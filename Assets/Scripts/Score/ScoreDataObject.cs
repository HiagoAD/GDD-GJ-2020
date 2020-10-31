using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ScoreData", menuName = "ScriptableObjects/Injectables/ScoreDataObject")]
public class ScoreDataObject : ScriptableObject {
	public int score = 0;

	public void ResetScoreData () {
		this.score = 0;
	}

	public void UpdateHighscore (ScoreDataObject newScore) {
		this.score = Mathf.Max (this.score, newScore.score);
	}

	private void OnEnable () {
		hideFlags = HideFlags.DontUnloadUnusedAsset;
	}
}