using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("�C���X�^���X���擾���₷���悤��")] public static GameManager Instance;

    [Tooltip("�e�v���C���[�̃A�N�V����")] bool[] _isAction = new bool[2];
    [Tooltip("��s����U��")] PlayerMode[] _playerModes = new PlayerMode[] {PlayerMode.Attack, PlayerMode.Protect};
    [Tooltip("�U������v���C���[�̔ԍ�")] int _attackPlayerNum;
    [Tooltip("�h�䂷��v���C���[�̔ԍ�")] int _protectPlayerNum;
    [Tooltip("false:Player1 true:Player2 �e���s")] bool _isWinPlayer;

    [Tooltip("�U���\����"), SerializeField] float _attackTime = 1.0f; 
    [Tooltip("�U������")]�@float _time;

    [Tooltip("�Q�[���X�^�[�g������")] bool _isGameStart;
    [Tooltip("�Q�[���̍ŏ����ǂ���")] bool _isGameFirst;

    [SerializeField] WinnerUI _winnerUI;
    [SerializeField] TimeCountController _tcc;

    int[] _playerWinCount = new int[2];

    //�J�v�Z����
    public bool IsGameStart => _isGameStart;
    public bool IsGameFirst => _isGameFirst;

    void Awake()
    {
        // �����C���X�^���X������Δj��
        if (Instance)
        {
            Debug.LogWarning("�C���X�^���X�����̂��ߔj��");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ChangeGameStart(bool flag)
    {
        _isGameStart = flag;
    }

    public void ChangeGameFirst(bool flag)
    {
        _isGameFirst = flag;
    }

    public void SetUp()
    {
        PlayerActionClear();
        ChangePlayerMode();
        _winnerUI.Clear();
        _tcc.enabled = true;
        _tcc.Start();
        Debug.Log("Player" + _attackPlayerNum + 1 + ":�U��, Player" + _protectPlayerNum + 1 + ":�h�q");
    }

    /// <summary>
    /// Player�̃A�N�V�������󂯎��
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
    /// Player�̃A�N�V�������N���A
    /// </summary>
    public void PlayerActionClear()
    {
        _isAction[0] = false;
        _isAction[1] = false;
    }

    /// <summary>
    /// �Q�[������
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
                ChangeGameStart(false);
                _winnerUI.IsWinnerUI(_isWinPlayer);
            }
            
            if(_time >= _attackTime)
            {
                _isWinPlayer = false;
                ChangeGameStart(false);
                _winnerUI.IsWinnerUI(_isWinPlayer);
            }
        }
    }

    /// <summary>
    /// �v���C���[�̍U������
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
    /// Player�U����`
    /// </summary>
    public void PlayerModeNumSet()
    {
        _attackPlayerNum = Array.IndexOf(_playerModes, PlayerMode.Attack);
        _protectPlayerNum = Array.IndexOf(_playerModes, PlayerMode.Protect);
    }

    /// <summary>
    /// Player�̍U��
    /// </summary>
    public enum PlayerMode
    {
        Attack,
        Protect,
    }
}
