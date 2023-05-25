using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private float seMinimumDistance;
    [SerializeField] AudioSource[] se;
    [SerializeField] AudioSource[] bgm;

    public bool playBgm;
    private int bgmIndex;

    private bool canPlaySE;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }

        Invoke("AllowSE", 1f);
    }

    private void Update()
    {
        if (!playBgm)
        {
            StopALlBGM();
        }
        else
        {
            if (!bgm[bgmIndex].isPlaying)
            {
                PlayBGM(bgmIndex);
            }
        }
    }

    public void PlaySE(int _seIndex, Transform _source)
    {

        /*
        if (se[_seIndex].isPlaying)
        {
            return;
        }
        */

        if (canPlaySE == false)
        {
            return;
        }

        if (_source!=null && Vector2.Distance(PlayerManager.instance.player.transform.position, _source.position) > seMinimumDistance)
        {
            return;
        }

        if (_seIndex < se.Length)
        {
            //se[_seIndex].pitch = Random.Range(0.85f, 1.1f);
            se[_seIndex].Play();
        }
    }

    public void StopSE(int _index) => se[_index].Stop();

    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;

        StopALlBGM();
        bgm[bgmIndex].Play();
    }

    public void StopALlBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    private void AllowSE() => canPlaySE = true;
}
