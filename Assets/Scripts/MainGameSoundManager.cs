using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class MainGameSoundManager : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] AudioSource startClip;
    [SerializeField] AudioSource loopClip;

    [Header("SFX")]
    [SerializeField] AudioClip lifeLoss = null;
    [SerializeField] AudioClip[] unitDefeated = null;

    private void Start()
    {
        startClip.Play();
        StartCoroutine(PlayLoop());
    }

    IEnumerator PlayLoop()
    {
        yield return new WaitForSeconds(startClip.clip.length - 0.5f);
        loopClip.Play();
    }

    public void PlayLifeLoss() {
        this.startClip.PlayOneShot(this.lifeLoss);
    }

    public void PlayUnitDefeatedSFX() {
        AudioClip chosenAudio = ChooseArrayElement(this.unitDefeated);
        this.startClip.PlayOneShot(chosenAudio);
    }

    private T ChooseArrayElement<T>(T[] array) {
        int index = Random.Range(0, array.Length);
        return array[index];
    }
}
