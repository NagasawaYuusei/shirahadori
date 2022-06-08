using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSystem : MonoBehaviour
{
    [SerializeField] TimeCountController _tcc;
    [SerializeField] SceneController _sc;
    void Update()
    {
        if(GameManager.Instance.IsNowGame)
        {
            GameManager.Instance.WhichPlayerWin();
        }
        else
        {
            if(Input.GetButtonDown("Jump"))
            {
                _tcc.GameStart();
            }

            if(Input.GetButtonDown("Cancel"))
            {
                _sc.ChangeScene("Title");
            }
        }
    }
}
