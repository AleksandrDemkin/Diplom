using UnityEngine;

public class MoveTest : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _sideSpeed = 6f;
    
    private Vector3 _direction;
    private Vector3 _offset;

    private bool _movingAuto;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        
        _movingAuto = false;
        
        CameraOffset();
    }
    
    public void FixedUpdate()
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
        
        /*float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 tempVect = new Vector3(h, 0, v).normalized * _speed * Time.deltaTime;
        _rigidbody.MovePosition(transform.position + tempVect);*/
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
        _direction.x = Input.GetAxisRaw("Horizontal") * _sideSpeed;
        //_direction.z = Speed;
        _direction.z = Input.GetAxisRaw("Vertical") * _speed;
        _playerRigidbody.velocity = _direction;
    }
        
    private void MovingAuto()
    {
        _direction = _playerRigidbody.velocity;
        _direction.x = Input.GetAxisRaw("Horizontal") * _sideSpeed;
        _direction.z = _speed;
        //_direction.z = Input.GetAxisRaw("Vertical") * Speed;
        _playerRigidbody.velocity = _direction;
    }
}
