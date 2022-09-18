using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SceneLoaderNetwork : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _onlineStartButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _mainMenuButton;
        
        [SerializeField] private Canvas _quitCanvas;
        [SerializeField] private Canvas _onlineCanvas;
        
        [SerializeField] private AudioClip _buttonClickClip;
        //[SerializeField] private AudioClip _mainMenuClip;
        [SerializeField] [Range(0.0f, 1.0f)] private float _buttonClickVolume = 0.7f;
        //[SerializeField] [Range(0.0f, 1.0f)] float _mainMenuVolume = 0.2f;
        
        private static AudioSource _audioSource;
        //private static AudioSource _mainMenuClipSound;
        private string _sceneName = "GameScene";
        private int _waitTime = 5;
        public static int _waitTimeLoad = 1;
        private int _waitTimeAsync = 3000;

        private void Start()
        {
            _quitCanvas.gameObject.SetActive(false);
            _onlineCanvas.gameObject.SetActive(false);
            
            _startButton.onClick.AddListener(GameSceneLoad);
            _saveButton.onClick.AddListener(SaveGame);
            _quitButton.onClick.AddListener(GameQuitAsync);
            _onlineStartButton.onClick.AddListener(OnlineCanvasActive);
            _mainMenuButton.onClick.AddListener(OnlineCanvasInactive);

            _audioSource = GetComponent<AudioSource>();
            if (_buttonClickClip != null) 
                _audioSource.clip = _buttonClickClip;
            
            _audioSource.playOnAwake = false;
            _audioSource.volume = _buttonClickVolume;
            
            /*_mainMenuClipSound = GetComponent<AudioSource>();
            if (_mainMenuClip != null) 
                _mainMenuClipSound.clip = _mainMenuClip;
            
            _mainMenuClipSound.playOnAwake = false;
            _mainMenuClipSound.volume = _mainMenuVolume;*/
            
            //MainMenuPlay();
        }

        private IEnumerator CoroutineLoadScene()
        {
            MyOnClick();
            yield return new WaitForSecondsRealtime(_waitTimeLoad);
            var asyncLoad = SceneManager.LoadSceneAsync(_sceneName);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        private void GameSceneLoad()
        {
            StartCoroutine(CoroutineLoadScene());
        }

        private void SaveGame()
        {
            MyOnClick();
            Debug.Log("Progress has been saved.");
        }

        #region GameQuitAsync

        async void GameQuitAsync()
        {
            MyOnClick();
            await Task.Delay(_waitTimeLoad);
            _quitCanvas.gameObject.SetActive(true);
            await Task.Delay(_waitTimeAsync);
            _quitCanvas.gameObject.SetActive(false);
            Application.Quit();
        }

        #endregion

        #region CoroutineGameQuit
        
        private IEnumerator CoroutineGameQuit()
        {
            MyOnClick();
            yield return new WaitForSecondsRealtime(_waitTimeLoad);
            QitCanvasActive();
            yield return new WaitForSeconds(_waitTime);
            QitCanvasInactive();
            Application.Quit();
        }
        
        private void GameQuit()
        {
            StartCoroutine(CoroutineGameQuit());
        }
        
        #endregion

        private void QitCanvasActive()
        {
            _quitCanvas.gameObject.SetActive(true);
        }
        
        private void QitCanvasInactive()
        {
            _quitCanvas.gameObject.SetActive(false);
        }
        
        private void OnlineCanvasActive()
        {
            MyOnClick();
            _onlineCanvas.gameObject.SetActive(true);
        }
        
        private void OnlineCanvasInactive()
        {
            MyOnClick();
            _onlineCanvas.gameObject.SetActive(false);
        }

        private static void MyOnClick()
        {
            _audioSource.Play();
        }
        
        /*public static void MainMenuPlay()
        {
            _mainMenuClipSound.Play();
        }*/
        
        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _onlineStartButton.onClick.RemoveAllListeners();
            _saveButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
            StopAllCoroutines();
        }
    }
}