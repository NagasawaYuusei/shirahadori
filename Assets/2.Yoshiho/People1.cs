using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People1 : MonoBehaviour
{
    [SerializeField] int _playerNum;

    void Update()
    {
        if (!GameManager.Instance.IsGameStart)
            return;
        Active();
    }
    /// <summary>
    /// プレイヤーのアクション
    /// </summary>
    void Active()
    {
        if (Input.GetButtonDown("Player" + _playerNum + "Active"))
        {
            Debug.Log("Player" + _playerNum +"がアクションしました");
            if(_playerNum == 1)
            {
                GameManager.Instance.PlayerAction(false);
            }
            else
            {
                GameManager.Instance.PlayerAction(true);
            }
        }
    }
 }