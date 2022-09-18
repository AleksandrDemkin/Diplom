using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Canvas _loseCanvas;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] [Range(0.0f, 1.0f)] float _volume = 0.5f;

        private AudioSource _audioSource;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioClip != null) 
                _audioSource.clip = _audioClip;
        
            _audioSource.playOnAwake = false;
            _audioSource.volume = _volume;
            _audioSource.Stop();
        }

        private void OnTriggerEnter(Collider player)
        {
            if (!player.GetComponent<Player>()) return;
            _audioSource.Play();
            
            _loseCanvas.gameObject.SetActive(true);
        }
    }
}