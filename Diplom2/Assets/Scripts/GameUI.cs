using System;
using System.Threading.Tasks;
using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _menuWinButton;
        [SerializeField] private Button _menuLoseButton;
        [SerializeField] private Canvas _winCanvas;
        [SerializeField] private Canvas _loseCanvas;
        [SerializeField] private TMP_Text _winText;
        [SerializeField] private TMP_Text _loseText;
    
        [SerializeField] private AudioClip _buttonClickClip;
        [SerializeField] [Range(0.0f, 1.0f)] float _buttonClickVolume = 0.7f;
        
        private static AudioSource _buttonClickSound;
        private string _textWin;
        private string _textLose;
        private static string _startSceneName;
        private static string _gameOverSceneName;
        private int _waitTime = 3;
        private int _waitTimeLoad = 2;
        
        private void Start()
        {
            _textWin = "You win! Try again.";
            _textLose = "You lose. Try again!";

            _winText.text = _textWin;
            _loseText.text = _textLose;
            
            _startSceneName = "StartScene";
            _gameOverSceneName = "GameOverScene";
            
            _menuButton.onClick.AddListener(LoadStartScene);
            _menuWinButton.onClick.AddListener(LoadGameOverScene);
            _menuLoseButton.onClick.AddListener(LoadGameOverScene);
            
            _buttonClickSound = GetComponent<AudioSource>();
            if (_buttonClickClip != null) 
                _buttonClickSound.clip = _buttonClickClip;
        
            _buttonClickSound.playOnAwake = false;
            _buttonClickSound.volume = _buttonClickVolume;
        }

        #region StartSceneLoad

        private IEnumerator CoroutineLoadScene()
        {
            MyOnClick();
            yield return new WaitForEndOfFrame();
            var asyncLoad = SceneManager.LoadSceneAsync(_startSceneName);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
       
        private void LoadStartScene()
        {
            StartCoroutine(CoroutineLoadScene());
        }
        
        #endregion
        
        #region CoroutineLoadGameOverScene

        private IEnumerator CoroutineLoadGameOverScene()
        {
            MyOnClick();
            yield return new WaitForEndOfFrame();
            var asyncLoad = SceneManager.LoadSceneAsync(_gameOverSceneName);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
       
        private void LoadGameOverScene()
        {
            if (_winCanvas || _loseCanvas)
            {
                StartCoroutine(CoroutineLoadGameOverScene());
            }
        }
        
        #endregion

        private static void MyOnClick()
        {
            _buttonClickSound.Play();
        }
        
        private void OnDestroy()
        {
            _menuButton.onClick.RemoveAllListeners();
            _menuWinButton.onClick.RemoveAllListeners();
            _menuLoseButton.onClick.RemoveAllListeners();
            StopAllCoroutines();
        }
    }
}