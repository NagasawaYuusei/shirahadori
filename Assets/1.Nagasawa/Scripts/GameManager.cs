using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("インスタンスを取得しやすいように")] public static GameManager Instance;
    [Tooltip("false:Player1 true:Player2 各勝敗")] bool _isWinPlayer;
    [Tooltip("各プレイヤーのアクション")] bool[] _isAction = new bool[2];
    //[Tooltip("")] 
    [Tooltip("先行か後攻か")] PlayerState[] _playerModes;
    [Tooltip("攻撃するプレイヤーの番号")] int _attackPlayerNum;
    [Tooltip("防御するプレイヤーの番号")] int _protectPlayerNum;
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

    public void WhichPlayerWin()
    {
        //if()
    }

    /// <summary>
    /// Player攻守を定義
    /// </summary>
    public void PlayerModeNumSet()
    {
        _attackPlayerNum = Array.IndexOf(_playerModes, PlayerState.Attack);
        _protectPlayerNum = Array.IndexOf(_playerModes, PlayerState.Protect);
    }

    public enum PlayerState
    {
        Attack,
        Protect,
    }
}
