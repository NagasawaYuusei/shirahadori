using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleController : MonoBehaviour
{
    [SerializeField] SceneController _sc;
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            _sc.ChangeScene("Title");
        }
    }
}
