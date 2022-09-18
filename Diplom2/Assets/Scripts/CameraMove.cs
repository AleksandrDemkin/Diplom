using UnityEngine;

namespace DefaultNamespace
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] Transform _lookAt;
        [SerializeField] Transform _player;
        
        private const float _yMin = -50.0f;
        private const float _yMax = 50.0f;
 
        public float _distance = 10.0f;
        private float _currentX = 0.0f;
        private float _currentY = 0.0f;
        public float _sensivity = 4.0f;
        
        void LateUpdate()
        {
 
            _currentX += Input.GetAxis("Mouse X") * _sensivity * Time.deltaTime;
            _currentY += Input.GetAxis("Mouse Y") * _sensivity * Time.deltaTime;
 
            _currentY = Mathf.Clamp(_currentY, _yMin, _yMax);
 
            Vector3 direction = new Vector3(0, 0, _distance);
            Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
            transform.position = _lookAt.position + rotation * direction;

            transform.LookAt(_lookAt.position);
        }
    }
}