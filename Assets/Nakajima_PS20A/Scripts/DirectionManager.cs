using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;

public class DirectionManager : MonoBehaviour
{
    [SerializeField]
    PlayableDirector m_director = default;

    [SerializeField]
    GameObject m_directionPanel = default;

    [SerializeField]
    GameObject m_slashEffect = default;

    [SerializeField]
    AudioClip m_seClip = default;

    [Header("Debug")]
    [SerializeField]
    bool m_isDebug = false;

    public static DirectionManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (m_isDebug)
        {
            StartCoroutine(DebugPlay());
        }
    }

    /// <summary>
    /// 演出再生
    /// </summary>
    public void OnPlayDirection()
    {
        m_director.Play();
    }

    /// <summary>
    /// 勝者の画像を表示する
    /// </summary>
    public void ViewResult()
    {
        m_slashEffect.SetActive(false);
        GameManager.Instance.ChangeNowGame(false);
        GameManager.Instance.WinUI();
        //Debug.Log("勝者UI表示");
    }

    /// <summary>
    /// SE再生
    /// </summary>
    public void PlaySE()
    {
        SoundManager.Instance.PlaySeByName(m_seClip.name);
    }

    IEnumerator DebugPlay()
    {
        yield return new WaitForSeconds(1.0f);

        OnPlayDirection();
    }
}
