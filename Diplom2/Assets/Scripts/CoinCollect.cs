using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class CoinCollect: MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private Camera _camera;
        [SerializeField] private Canvas _winCanvas;
        [SerializeField] private AudioSource _winSound;
        
        private Coin _coin;
        private float _speed;
        private float _time;
        private int _distance;
        
        #region CoinsCount

        [SerializeField] private TMP_Text _coinsCount;
        [SerializeField] private TMP_Text _coinsToWin;
        [SerializeField] AudioClip _coinDropClip;
        [SerializeField] [Range(0.0f, 1.0f)] float _volume = 0.5f;

        private static AudioSource _coinDrop;
        private int _coinsAmount;
        private int _coinCost;
        private int _coinsToWinAmount;
        private Color _color = Color.white;
        private IEnumerator _coroutineLoadGameOverScene;

        #endregion

        private void Start()
        {
            _coinDrop = GetComponent<AudioSource>();
            if (_coinDropClip != null) 
                _coinDrop.clip = _coinDropClip;
        
            _coinDrop.playOnAwake = false;
            _coinDrop.volume = _volume;
            
            _speed = 1f;
            _distance = -1;
            
            if (_camera == null)
            {
                _camera = Camera.main;
            }
            
            _coinsAmount = 0;
            _coinCost = 1;
            _coinsCount.color = _color;
            _coinsCount.text = $"Coins: {_coinsAmount.ToString()}";
        
            _coinsToWinAmount = 20;
            _coinsToWin.color = _color;
            _coinsToWin.text = $"Coins to win: {_coinsToWinAmount.ToString()}";
        }

        public void CoinStartMove(Vector3 initial, Action onComplete)
        {
            var position = _camera.transform.position;
            var targetPos = _camera.ScreenToWorldPoint(new Vector3(_target.position.x, _target.position.y,
                position.z * _distance));
            var coin = Instantiate(_coinPrefab, transform);

            StartCoroutine(CoinMove(coin.transform, initial, targetPos, onComplete));
        }

        private IEnumerator CoinMove(Transform obj, Vector3 startPosition, Vector3 endPosition, Action onComplete)
        {
            _time = 0;
            
            while (_time < 1)
            {
                _time += _speed * Time.deltaTime;
                obj.position = Vector3.Lerp(startPosition, endPosition, _time);

                yield return new WaitForEndOfFrame();
            }
            
            onComplete.Invoke();
            _coinDrop.Play();
            //Destroy(obj.gameObject);
            obj.gameObject.SetActive(false);
        }

        public void CountCoinsAmount()
        {
            if (_coinsAmount < _coinsToWinAmount)
            {
                _coinsAmount += _coinCost;
                
                _coinsCount.text = $"Coins: {_coinsAmount.ToString()}";
                _coinsToWin.text = $"Coins to win: {_coinsToWinAmount.ToString()}";
            }

            if (_coinsAmount == _coinsToWinAmount)
            {
                _winCanvas.gameObject.SetActive(true);
                _winSound.Play();
            }
        }
    }
}