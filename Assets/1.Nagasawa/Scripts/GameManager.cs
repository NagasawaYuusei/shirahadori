using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("インスタンスを取得しやすいように")] public static GameManager Instance;

    [Tooltip("各プレイヤーのアクション")] bool[] _isAction = new bool[2];
    [Tooltip("先行か後攻か")] PlayerMode[] _playerModes = new PlayerMode[] {PlayerMode.Protect, PlayerMode.Attack};
    [Tooltip("攻撃するプレイヤーの番号")] int _attackPlayerNum;
    [Tooltip("防御するプレイヤーの番号")] int _protectPlayerNum;
    [Tooltip("false:Player1 true:Player2 各勝敗")] bool _isWinPlayer;

    [Tooltip("攻撃可能時間"), SerializeField] float _attackTime = 1.0f; 
    [Tooltip("攻撃時間")]　float _time;

    [Tooltip("ゲームスタートしたか")] bool _isNowGame;

    [SerializeField] WinnerUI _winnerUI;
    [SerializeField] TimeCountController _tcc;

    int[] _playerWinCount = new int[2];
    bool _playerMiss;

    [SerializeField] SpriteRenderer _playerSprite;
    [SerializeField] Sprite _attackWinSprite;
    [SerializeField] Sprite _protectWinSprite;
    [SerializeField] Sprite _defaultPlayerSprite;

    bool a;
    bool b;

    //カプセル化
    public bool IsNowGame => _isNowGame;
    public bool IsWinPlayer => _isWinPlayer;

    void Awake()
    {
        // 同じインスタンスがあれば破棄
        if (Instance)
        {
            Debug.LogWarning("インスタンス複数のため破棄");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// ゲーム開始時のflag情報変更メソッド
    /// </summary>
    /// <param name="flag"></param>
    public void ChangeNowGame(bool flag)
    {
        _isNowGame = flag;
    }

    public void ReStartSetUp()
    {
        a = false;
        b = false;
        PlayerActionClear();
        _playerSprite.sprite = _defaultPlayerSprite;
    }

    /// <summary>
    /// Playerのアクションを受け取る
    /// false:Player1 true:Player2
    /// </summary>
    public void PlayerAction(bool isPlayer)
    {
        if(!isPlayer)
        {
            _isAction[0] = true;
        }
        else
        {
            _isAction[1] = true;
        }
    }

    /// <summary>
    /// Playerのアクションをクリア
    /// </summary>
    public void PlayerActionClear()
    {
        _isAction[0] = false;
        _isAction[1] = false;
        _time = 0;
    }

    public void PlayerSpriteChange()
    {
        if(!_isWinPlayer)
        {
            if(_attackPlayerNum == 0)
            {
                _playerSprite.sprite = _attackWinSprite;
            }
            else
            {
                _playerSprite.sprite = _protectWinSprite;
            }
        }
        else
        {
            if (_attackPlayerNum == 1)
            {
                _playerSprite.sprite = _attackWinSprite;
            }
            else
            {
                _playerSprite.sprite = _protectWinSprite;
            }
        }
    }

    /// <summary>
    /// ゲーム処理
    /// </summary>
    public void WhichPlayerWin()
    {
        if(_isAction[_attackPlayerNum] && !a)
        {
            Debug.Log("AttackNow");
            _time += Time.deltaTime;
            if(!b)
            {
                DirectionManager.Instance.OnPlayDirection();
                b= true;
            }
            if(_isAction[_protectPlayerNum])
            {
                Debug.Log("ProtectNow");
                if(_protectPlayerNum == 0)
                {
                    _isWinPlayer = false;
                }
                else
                {
                    _isWinPlayer = true;
                }
                PlayerSpriteChange();
                a = true;
            }
            
            if(_time >= _attackTime)
            {
                Debug.Log("OverTime");
                if (_protectPlayerNum == 0)
                {
                    _isWinPlayer = true;
                }
                else
                {
                    _isWinPlayer = false;
                }
                DirectionManager.Instance.OnPlayDirection();
                PlayerSpriteChange();
            }
        }
        else if(_isAction[_protectPlayerNum] && !a)
        {
            Debug.Log("ProtectPlayerMiss");
            if (_protectPlayerNum == 0)
            {
                _isWinPlayer = true;
            }
            else
            {
                _isWinPlayer = false;
            }
            _playerMiss = true;
            GameManager.Instance.ChangeNowGame(false);
            GameManager.Instance.WinUI();
            SoundManager.Instance.PlaySeByName("Miss");
        }
    }

    /// <summary>
    /// WinnerUI表示
    /// </summary>
    public void WinUI()
    {
        _winnerUI.IsWinnerUI(_isWinPlayer);
    }

    /// <summary>
    /// プレイヤーの攻守を交代
    /// </summary>
    public void ChangePlayerMode()
    {
        for(int i = 0; i < 2; i++)
        {
            if(_playerModes[i] == PlayerMode.Attack)
            {
                _playerModes[i] = PlayerMode.Protect;
            }
            else
            {
                _playerModes[i] = PlayerMode.Attack;
            }
        }
    }

    /// <summary>
    /// Player攻守を定義
    /// </summary>
    public void PlayerModeNumSet()
    {
        _attackPlayerNum = Array.IndexOf(_playerModes, PlayerMode.Attack);
        _protectPlayerNum = Array.IndexOf(_playerModes, PlayerMode.Protect);
    }

    /// <summary>
    /// Playerの攻守
    /// </summary>
    public enum PlayerMode
    {
        Attack,
        Protect,
    }
}
