using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountController : MonoBehaviour
{
    [SerializeField] Text _timeCountText;
    float _firstTime = 3.9f;
    float _time;
    [SerializeField] float _startTimeTextDenote;
    [SerializeField] GameObject _startButton;

    bool _isGameStart;
    bool _isFinishCount;



    public void Start()
    {
        _timeCountText.enabled = true;
        _timeCountText.text = "èÄîıÇÕÇ¢Ç¢Ç©ÅH";
        _time = _firstTime;
        _isGameStart = false;
    }
    void Update()
    {
        if (!_isGameStart)
            return;

        if (!_isFinishCount)
        {
            _time -= Time.deltaTime;
            int timeInt = (int)_time;
            _timeCountText.text = timeInt.ToString();
            if(_time < 1)
            {
                _isFinishCount = true;
                _time = 0;
                _timeCountText.text = "énÇﬂÅI";
            }
        }
        else if(_isFinishCount)
        {
            _time += Time.deltaTime;
            if (_time >= _startTimeTextDenote)
            {
                _time = 0;
                _timeCountText.enabled = false;
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
