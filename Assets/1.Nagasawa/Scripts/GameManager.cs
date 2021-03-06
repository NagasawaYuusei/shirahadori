using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("インスタンスを取得しやすいように")] public static GameManager Instance;

    [Tooltip("各プレイヤーのアクション")] bool[] _isAction = new bool[2];
    [Tooltip("先行か後攻か")] PlayerMode[] _playerModes = new PlayerMode[] {PlayerMode.Attack, PlayerMode.Protect};
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

    public void SetUp()
    {
        PlayerActionClear();
        ChangePlayerMode();
        _winnerUI.Clear();
        _tcc.enabled = true;
        _tcc.Start();
        Debug.Log("Player" + _attackPlayerNum + 1 + ":攻撃, Player" + _protectPlayerNum + 1 + ":防衛");
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
    }

    /// <summary>
    /// ゲーム処理
    /// </summary>
    public void WhichPlayerWin()
    {
        if(_isAction[_attackPlayerNum])
        {
            Debug.Log("AttackNow");
            _time += Time.deltaTime;
            if(_isAction[_protectPlayerNum])
            {
                Debug.Log("ProtectNow");
                _isWinPlayer = true;
            }
            
            if(_time >= _attackTime)
            {
                Debug.Log("OverTime");
                _isWinPlayer = false;
            }
        }
        else if(_isAction[_protectPlayerNum])
        {
            Debug.Log("ProtectPlayerMiss");
            _isWinPlayer = false;
            _playerMiss = true;
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
