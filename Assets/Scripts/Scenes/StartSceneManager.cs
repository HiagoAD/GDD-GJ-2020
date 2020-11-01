using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour {
	[SerializeField] private AudioSource audioSource = null;
	[SerializeField] private AudioClip clickSFX = null;
	public void PlayClickSFX() {
		this.audioSource.PlayOneShot(this.clickSFX);
	}

	public void MoveToGameScene () {
		SceneManager.LoadScene (Constants.Scenes.GAME);
	}
}