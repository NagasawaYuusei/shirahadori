using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerUI : MonoBehaviour
{
    [SerializeField] Text _winnerText;

    void Start()
    {
        Clear();
    }

    public void Clear()
    {
        _winnerText.text = "";
    }

    /// <summary>
    /// 勝敗が決まった時に呼び出す
    /// Player1ならfalse,Player2ならtrue
    /// </summary>
    /// <param name="isWinner"></param>
    public void IsWinnerUI(bool isWinner)
    {
        if(!isWinner)
        {
            _winnerText.text = "Player1 Win";
        }
        else
        {
            _winnerText.text = "Player2 Win";
        }
    }
}
