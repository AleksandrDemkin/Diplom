using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerMove : MonoBehaviour
    {
        private CharacterController _characterControllerontroller;

        [SerializeField] private float _speed = 10f;
        [SerializeField] private Transform _camera;
        [SerializeField] private CoinCollect _coinCollect;

        private void Start()
        {
            _characterControllerontroller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            float Horizontal = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
            float Vertical = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
 
            Vector3 Movement = _camera.transform.right * Horizontal + _camera.transform.forward * Vertical;
            Movement.y = 0f;
 
 
            _characterControllerontroller.Move(Movement);

            if (Movement.magnitude != 0f)
            {
                transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 
                                 _camera.GetComponent<CameraMove>()._sensivity * Time.deltaTime);
                
                Quaternion CamRotation = _camera.rotation;
                CamRotation.x = 0f;
                CamRotation.z = 0f;

                transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);
            }
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