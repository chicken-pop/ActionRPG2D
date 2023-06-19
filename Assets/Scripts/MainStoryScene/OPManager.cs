using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OPManager : MonoBehaviour
{
    [SerializeField] private StoryManager storyManager;

    private void Start()
    {
        //OPを表示させない
        if(GameProgressManager.Instance.flagList.Flags[0].IsOn == true)
        {
            this.gameObject.SetActive(false);
        }
        //OPを表示させる
        if (GameProgressManager.Instance.flagList.Flags[0].IsOn == false)
        {
            StartCoroutine(FinishOp());
        }
    }

    private IEnumerator FinishOp()
    {
        //yield return new WaitForSeconds((float)this.gameObject.GetComponent<VideoPlayer>().clip.length); //後で直す
        yield return new WaitForSeconds(3f);
       
        this.gameObject.SetActive(false);

        storyManager.storyIndex++;
        storyManager.StoryProgression(storyManager.storyIndex);

        GameProgressManager.Instance.SetFlag(0); //OPフラグを立てる
    }


}
