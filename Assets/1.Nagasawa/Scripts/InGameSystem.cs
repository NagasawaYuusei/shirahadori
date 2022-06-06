using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSystem : MonoBehaviour
{
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

            }
        }
    }
}
