using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPManager : MonoBehaviour
{
    [SerializeField] private StoryManager storyManager;

    private void Start()
    {
        if (this.gameObject.activeSelf == true)
        {
            Invoke("FinishOp", 3f);
        }
    }

    private void FinishOp()
    {
        this.gameObject.SetActive(false);

        storyManager.storyIndex++;
        storyManager.StoryProgression(storyManager.storyIndex);
    }


}
