using UnityEngine;

namespace DefaultNamespace
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int _coinAnimation = Animator.StringToHash("CoinAnimation");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.Play(_coinAnimation);
        }
    }
}