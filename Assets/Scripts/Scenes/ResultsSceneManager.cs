using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsSceneManager : MonoBehaviour {
	[SerializeField] private Text scoreText = null;
	[SerializeField] private ScoreDataObject scoreData = null;
	[SerializeField] private AudioSource audioSource = null;
	[SerializeField] private AudioClip clickSFX = null;

	void Start () {
		this.scoreText.text = "Danilos sóbrios: " + this.scoreData.score;
	}

	public void PlayClickSFX() {
		this.audioSource.PlayOneShot(this.clickSFX);
	}

	public void MoveToGameScene () {
		SceneManager.LoadScene (Constants.Scenes.GAME);
	}
}