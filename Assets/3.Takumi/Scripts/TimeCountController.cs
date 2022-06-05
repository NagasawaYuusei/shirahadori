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

    void Start()
    {
        int firstTimeInt = 0;
        firstTimeInt = (int)_firstTime;
        _timeCountText.text = firstTimeInt.ToString();
        _time = _firstTime;
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
                _timeCountText.enabled = false;
                _isStartText = false;
            }
        }
    }

    public void GameStart()
    {
        _isGameStart = true;
    }
}
