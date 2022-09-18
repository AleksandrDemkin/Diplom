using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CoinCollect _coinCollect;
    
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private Vector3 _offset;
    private bool _groundedPlayer;
    private float _playerSpeed = 10.0f;
    private float _jumpHeight = 1.0f;
    private float _gravityValue = -9.81f;
    private string _jump = "Jump";
    private string _horizontal = "Horizontal";
    private string _vertical = "Vertical";
    
    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        
        CameraOffset();
    }
    
    private void FixedUpdate()
    {
        Move();
        CameraPosition();
    }

    private void Move()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis(_horizontal), 0, Input.GetAxis(_vertical));
        _controller.Move(move * Time.deltaTime * _playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        
        if (Input.GetButtonDown(_jump) && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void CameraPosition()
    {
        _camera.transform.position = _controller.transform.position + _offset;
    }

    private void CameraOffset()
    {
        _offset = _camera.transform.position - _controller.transform.position;
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
