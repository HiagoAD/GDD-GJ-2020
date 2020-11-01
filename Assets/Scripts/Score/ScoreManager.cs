using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnScoreUpdatedEvent : UnityEvent<int> { }

public class ScoreManager : MonoBehaviour {
	[SerializeField] private ScoreDataObject scoreData = null;
	[SerializeField] private OnScoreUpdatedEvent onScoreUpdated = new OnScoreUpdatedEvent ();

	void Start () {
		this.scoreData.ResetScoreData ();
	}

	public void CountReturnedUnit() {
		this.scoreData.score++;
		this.onScoreUpdated.Invoke(this.scoreData.score);
	}
}