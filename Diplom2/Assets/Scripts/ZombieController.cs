using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ZombieController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed = 0.5f;
        [SerializeField] private Canvas _loseCanvas;
        [SerializeField] AudioClip _zRoarClip;
        [SerializeField] [Range(0.0f, 1.0f)] float _volume = 0.5f;
        
        
        //private Animator _animator;
        private AudioSource _zombieSound;
        //private static readonly int Attack = Animator.StringToHash("ZAttackAnimation");
        //private static readonly int Walk = Animator.StringToHash("ZWalkAnimation");
        

        private void Start()
        {
            //_animator = GetComponent<Animator>();
            _zombieSound = GetComponent<AudioSource>();
            if (_zRoarClip != null) 
                _zombieSound.clip = _zRoarClip;
        
            _zombieSound.playOnAwake = false;
            _zombieSound.volume = _volume;
            _zombieSound.Stop();
        }

        private void Update()
        {
            ZMove();
        }

        private void ZMove()
        {
            //ZWalk();
            TargetPosition();
        }

        private void ZWalk()
        {
            //_animator.Play(Walk);
        }

        private void TargetPosition()
        {
            transform.position = Vector3.MoveTowards(transform.position,
            _target.position, _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider player)
        {
            if (!player.GetComponent<Player>()) return;
            _zombieSound.Play();
            //_animator.Play(Attack);
            _loseCanvas.gameObject.SetActive(true);
        }
    }
}