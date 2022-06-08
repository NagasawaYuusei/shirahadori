using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("�C���X�^���X���擾���₷���悤��")] public static GameManager Instance;

    [Tooltip("�e�v���C���[�̃A�N�V����")] bool[] _isAction = new bool[2];
    [Tooltip("��s����U��")] PlayerMode[] _playerModes = new PlayerMode[] {PlayerMode.Protect, PlayerMode.Attack};
    [Tooltip("�U������v���C���[�̔ԍ�")] int _attackPlayerNum;
    [Tooltip("�h�䂷��v���C���[�̔ԍ�")] int _protectPlayerNum;
    [Tooltip("false:Player1 true:Player2 �e���s")] bool _isWinPlayer;

    [Tooltip("�U���\����"), SerializeField] float _attackTime = 1.0f; 
    [Tooltip("�U������")]�@float _time;

    [Tooltip("�Q�[���X�^�[�g������")] bool _isNowGame;

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

    //�J�v�Z����
    public bool IsNowGame => _isNowGame;
    public bool IsWinPlayer => _isWinPlayer;

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

    /// <summary>
    /// �Q�[���J�n����flag���ύX���\�b�h
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
    /// �Q�[������
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
    /// WinnerUI�\��
    /// </summary>
    public void WinUI()
    {
        _winnerUI.IsWinnerUI(_isWinPlayer);
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
