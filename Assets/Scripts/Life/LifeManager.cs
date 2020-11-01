using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class OnLifeUpdatedEvent : UnityEvent<int> { }

public class LifeManager : MonoBehaviour {
	[SerializeField] private int qtLives;
	[SerializeField] private int qtMaxLives = 3;
	[SerializeField] private OnLifeUpdatedEvent onLifeUpdated = new OnLifeUpdatedEvent ();

	void Start () {
		this.qtLives = this.qtMaxLives;
	}

	public void LoseLife () {
		this.qtLives--;
		this.onLifeUpdated.Invoke(this.qtLives);
		if (this.qtLives == 0)
			MoveToResultsScene ();
	}

	private void MoveToResultsScene () {
		SceneManager.LoadScene (Constants.Scenes.RESULTS);
	}
}