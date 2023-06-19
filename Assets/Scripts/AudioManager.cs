using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private float seMinimumDistance;

    [SerializeField] AudioSource audioSourceBGM = default;
    [SerializeField] AudioClip[] bgmClips;

    [SerializeField] private AudioSource audioSourceSE = default;
    [SerializeField] private AudioClip[] seClips;

    [SerializeField] private AudioMixerGroup bgmMixer;
    [SerializeField] private AudioMixerGroup seMixer;

    public enum BGM
    {
        Battle,
        Boss,
        Culuture,
        Assistant,
        Memories
    }

    public enum SE
    {
        sword,
        dash,
        button
    }

    private bool canPlaySE;


    private void Start()
    {
        audioSourceBGM.outputAudioMixerGroup = bgmMixer;
        audioSourceSE.outputAudioMixerGroup = seMixer;

        Invoke("AllowSE", 0.5f);

        //‰¼
        if (SceneManager.GetActiveScene().name == "MainBattleSceneForest")
        {
            //PlayBGM(BGM.Battle);

        }
    }



    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainBattleSceneForest")
        {
            //PlayBGM(BGM.Battle);

        }
    }

    public void PlayBGM(BGM bgm)
    {
        if (audioSourceBGM.clip != bgmClips[(int)bgm])
        {
            audioSourceBGM.clip = bgmClips[(int)bgm];
            audioSourceBGM.Play();
        }
    }

    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }

    public void PlaySE(SE se, Transform _source)
    {

        if (canPlaySE == false)
        {
            return;
        }

        if (_source != null && Vector2.Distance(PlayerManager.instance.player.transform.position, _source.position) > seMinimumDistance)
        {
            return;
        }

        audioSourceSE.PlayOneShot(seClips[(int)se]);
    }

    public void StopSE(SE se) => audioSourceSE.Stop();

    private void AllowSE() => canPlaySE = true;
}
