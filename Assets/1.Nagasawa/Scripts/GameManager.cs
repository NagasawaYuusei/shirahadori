using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("インスタンスを取得しやすいように")] public static GameManager Instance;
    [Tooltip("false:Player1 true:Player2 各勝敗")] bool _isWinPlayer;
    [Tooltip("各プレイヤーのアクション")] bool[] _isAction = new bool[2];
    [Tooltip("")] float _attackTime; 
    [Tooltip("先行か後攻か")] PlayerMode[] _playerModes;
    [Tooltip("攻撃するプレイヤーの番号")] int _attackPlayerNum;
    [Tooltip("防御するプレイヤーの番号")] int _protectPlayerNum;
    float _time;
    bool a;

    void Awake()
    {
        // 同じインスタンスがあれば破棄
        if (Instance)
        {
            Debug.LogWarning("インスタンス複数のため破棄");
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Playerのアクションを受け取る
    /// false:Player1 true:Player2
    /// </summary>
    public void PlayerAcrion(bool isPlayer)
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
            _time += Time.deltaTime;
            if(_isAction[_protectPlayerNum])
            {
                _isWinPlayer = false;
            }
            
            if(_time >= _attackTime)
            {
                _isWinPlayer = true;
            }
        }
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
