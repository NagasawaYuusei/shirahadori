using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People1 : MonoBehaviour
{
    [SerializeField] int _playerNum;

    private void Update()
    {
        Active();
    }
    /// <summary>
    /// �v���C���[�̃A�N�V����
    /// </summary>
    void Active()
    {
        if (Input.GetButtonDown("Player" + _playerNum + "Active"))
        {
            Debug.Log("Player" + _playerNum +"��Attack���܂���");
            if(_playerNum == 1)
            {
                GameManager.Instance.PlayerAcrion(false);
            }
            else
            {
                GameManager.Instance.PlayerAcrion(true);
            }
        }
    }
 }