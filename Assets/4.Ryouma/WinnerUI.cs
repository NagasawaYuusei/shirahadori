using UnityEngine;
using UnityEngine.UI;

public class WinnerUI : MonoBehaviour
{
    [SerializeField] Text _winnerText;
    [SerializeField] string[] _winPlayerText;
    [SerializeField] TimeCountController _tcc;

    void Start()
    {
        Clear();
    }

    public void Clear()
    {
        _winnerText.text = "";
    }

    /// <summary>
    /// ���s�����܂������ɌĂяo��
    /// Player1�Ȃ�false,Player2�Ȃ�true
    /// </summary>
    /// <param name="isWinner"></param>
    public void IsWinnerUI(bool isWinner)
    {
        if(!isWinner)
        {
            _winnerText.text = _winPlayerText[0];
            _tcc.Start();
            //GameManager.Instance
        }
        else
        {
            _winnerText.text = _winPlayerText[1];
            _tcc.Start();
        }
    }
}
