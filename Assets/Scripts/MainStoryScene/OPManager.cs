using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OPManager : MonoBehaviour
{
    [SerializeField] private StoryManager storyManager;

    private void Start()
    {
        if (this.gameObject.activeSelf == true)
        {
            StartCoroutine(FinishOp());
        }
    }

    private IEnumerator FinishOp()
    {
        //yield return new WaitForSeconds((float)this.gameObject.GetComponent<VideoPlayer>().clip.length); Œã‚Å’¼‚·
        yield return null;
       
        this.gameObject.SetActive(false);

        storyManager.storyIndex++;
        storyManager.StoryProgression(storyManager.storyIndex);
    }


}
