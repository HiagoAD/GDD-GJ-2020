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
}
