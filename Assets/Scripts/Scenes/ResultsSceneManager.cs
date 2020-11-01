using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsSceneManager : MonoBehaviour {
	[SerializeField] private Text scoreText = null;
	[SerializeField] private ScoreDataObject scoreData = null;

	void Start () {
		this.scoreText.text = "Danilos sóbrios: " + this.scoreData.score;
	}

	public void MoveToGameScene () {
		SceneManager.LoadScene (Constants.Scenes.GAME);
	}
}