using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sounds;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ChooseRandom()
    {
        var randomIndex = Random.Range(0, _sounds.Length);
        _audioSource.PlayOneShot(_sounds[randomIndex]);
    }
}
