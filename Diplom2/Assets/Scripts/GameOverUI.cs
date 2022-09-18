using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    
    [SerializeField] private TMP_Text _gameOverText;
    
    [SerializeField] private AudioClip _mainMenuClip;
    [SerializeField] [Range(0.0f, 1.0f)] float _volume = 0.3f;
    [SerializeField] private AudioSource _audioSource;

    private int _waitTimeLoad;
    private string _text;
    private string _startSceneName;

    private void Start()
    {
        _waitTimeLoad = 3;
        _startSceneName = "StartScene";
        _text = "Game over.\nYou can try again";
        
        _gameOverText.text = _text;
        
        
        _audioSource = GetComponent<AudioSource>();
        if (_mainMenuClip != null) 
            _audioSource.clip = _mainMenuClip;
        
        _audioSource.playOnAwake = false;
        _audioSource.volume = _volume;
        _audioSource.Play();
    }

    private void LateUpdate()
    {
        StartSceneLoad();
    }

    #region StartSceneLoad
    
    private IEnumerator CoroutineLoadScene()
    {
        yield return new WaitForSecondsRealtime(_waitTimeLoad);
        var asyncLoad = SceneManager.LoadSceneAsync(_startSceneName);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
    private void StartSceneLoad()
    {
        StartCoroutine(CoroutineLoadScene());
    }
            
    #endregion
    

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
