using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        [SerializeField] private CoinCollect _coinCollect;
        [SerializeField] private bool _movingAuto;
        
        private Rigidbody _playerRigidbody;
        private Vector3 _direction;
        private Vector3 _offset;
        private Vector3 _lightOffset;
        
        private const float Speed = 10f;
        private const float SideSpeed = 4f;
        
        private void Start()
        {
            _playerRigidbody = GetComponent<Rigidbody>();
            
            CameraOffset();
            
            _movingAuto = true;
        }

        private void FixedUpdate()
        {
            CameraPosition();
            
            if (!_movingAuto)
            {
                MovingManual();
            }
            else
            {
                MovingAuto();
            }
        }
        
        private void CameraPosition()
        {
            _camera.transform.position = 
                _playerRigidbody.transform.position + _offset;
        }

        private void CameraOffset()
        {
            _offset = 
                _camera.transform.position - _playerRigidbody.transform.position;
        }
        
        private void MovingManual()
        {
            _direction = _playerRigidbody.velocity;
            _direction.x = Input.GetAxisRaw("Horizontal") * SideSpeed;
            //_direction.z = Speed;
            _direction.z = Input.GetAxisRaw("Vertical") * Speed;
            _playerRigidbody.velocity = _direction;
        }
        
        private void MovingAuto()
        {
            _direction = _playerRigidbody.velocity;
            _direction.x = Input.GetAxisRaw("Horizontal") * SideSpeed;
            _direction.z = Speed;
            //_direction.z = Input.GetAxisRaw("Vertical") * Speed;
            _playerRigidbody.velocity = _direction;
        }

        private void OnTriggerEnter(Collider coin)
        {
            if (!coin.GetComponent<Coin>()) return;
            _coinCollect.CoinStartMove(coin.transform.position, () =>
            {
                _coinCollect.CountCoinsAmount();
            });
            coin.gameObject.SetActive(false);
        }
    }
}