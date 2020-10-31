using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeManager : MonoBehaviour {
	[SerializeField] private int qtLives;
	[SerializeField] private int qtMaxLives = 3;
	[SerializeField] private UnityEvent onLoseAllLives = new UnityEvent ();

	void Start () {
		this.qtLives = this.qtMaxLives;
	}

	public void LoseLife () {
		this.qtLives--;
		if (this.qtLives == 0)
			this.onLoseAllLives.Invoke ();
	}
}