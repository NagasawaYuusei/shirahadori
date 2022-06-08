using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] AudioClip _bgm;
    [SerializeField] bool _bigValue;
    void Start()
    {
        SoundManager.Instance.PlayBgmByName(_bgm.name);
        if(_bigValue)
        {
            SoundManager.Instance.BgmVolChange(0.7f);
        }
        else
        {
            SoundManager.Instance.BgmVolChange(0.3f);
        }
    }
}
