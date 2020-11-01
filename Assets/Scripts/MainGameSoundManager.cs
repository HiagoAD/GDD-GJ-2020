using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioListener))]
public class MainGameSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource startClip;
    [SerializeField] AudioSource loopClip;

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
}
