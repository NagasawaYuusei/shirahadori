using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSystem : MonoBehaviour
{
    void Update()
    {
        if(GameManager.Instance.IsGameStart)
        {
            GameManager.Instance.WhichPlayerWin();
        }
    }
}
