using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBackground : MonoBehaviour
{
    [SerializeField] private AudioClip[] _tracks;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Reset();
    }

    private void Reset()
    {
        var randomIndex = Random.Range(0, _tracks.Length);
        _audioSource.PlayOneShot(_tracks[randomIndex]);
        Invoke(nameof(Reset), _tracks[randomIndex].length); 
        print(_tracks[randomIndex].name);
    }
}
