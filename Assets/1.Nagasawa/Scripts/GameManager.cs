using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("�C���X�^���X���擾���₷���悤��")] public static GameManager Instance;
    [Tooltip("false:Player1 true:Player2 �e���s")] bool _isWinPlayer;
    [Tooltip("�e�v���C���[�̃A�N�V����")] bool[] _isAction = new bool[2];
    //[Tooltip("")] 
    [Tooltip("��s����U��")] PlayerState[] _playerModes;
    [Tooltip("�U������v���C���[�̔ԍ�")] int _attackPlayerNum;
    [Tooltip("�h�䂷��v���C���[�̔ԍ�")] int _protectPlayerNum;
    bool a;

    void Awake()
    {
        // �����C���X�^���X������Δj��
        if (Instance)
        {
            Debug.LogWarning("�C���X�^���X�����̂��ߔj��");
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Player�̃A�N�V�������󂯎��
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
    /// Player�̃A�N�V�������N���A
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
    /// Player�U����`
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
