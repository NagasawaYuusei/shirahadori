using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountController : MonoBehaviour
{
    [SerializeField] Text _timeCountText;
    bool _isGameStart;
    bool _isFinishCount;
    float _firstTime = 3.9f;
    float _time;
    bool _isStartText;
    [SerializeField] float _startTimeTextDenote;
    [SerializeField] GameObject _startButton;

    public void Start()
    {
        if (GameManager.Instance.IsGameFirst)
            return;
        _timeCountText.enabled = true;
        _timeCountText.text = "AreYouReady?";
        _time = _firstTime;
        GameManager.Instance.ChangeGameFirst(true);
        _startButton.SetActive(true);
        _isStartText = false;
        _isFinishCount = false;
        _isGameStart = false;
    }
    void Update()
    {
        if (_isGameStart && !_isFinishCount)
        {
            _time -= Time.deltaTime;
            int timeInt = (int)_time;
            _timeCountText.text = timeInt.ToString();
            if(_time < 1)
            {
                _isFinishCount = true;
                _isStartText = true;
                _time = 0;
                _timeCountText.text = "Start";
            }
        }

        if(_isStartText)
        {
            _time += Time.deltaTime;
            if(_time >= _startTimeTextDenote)
            {
                _time = 0;
                _timeCountText.enabled = false;
                _isStartText = false;
                _isGameStart = false;
                _isFinishCount = false;
                GameManager.Instance.ChangeGameStart(true);
                GameManager.Instance.PlayerModeNumSet();
            }
        }
    }

    public void GameStart()
    {
        _isGameStart = true;
        _startButton.SetActive(false);
    }
}
