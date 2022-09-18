using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _gameClip;
        [SerializeField] [Range(0.0f, 1.0f)] float _volume = 0.5f;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if (_gameClip != null) 
                _audioSource.clip = _gameClip;
        
            _audioSource.playOnAwake = false;
            _audioSource.volume = _volume;
            _audioSource.Play();
        }
    }
}