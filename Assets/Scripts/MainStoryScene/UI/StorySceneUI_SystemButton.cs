using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySceneUI_SystemButton : MonoBehaviour
{
    [SerializeField] private StoryManager storyManager;
    public GameObject autoCoverImage;
    public GameObject skipCoverImage;

    private void Start()
    {
        autoCoverImage.SetActive(false);
        skipCoverImage.SetActive(false);
    }

    public void Auto()
    {
        if (storyManager.isAuto == false)
        {
            storyManager.isAuto = true;
            autoCoverImage.SetActive(true);
        }
        else
        {
            storyManager.isAuto = false;
            autoCoverImage.SetActive(false);
        }
    }

    public void Skip()
    {
        if (storyManager.isSkip == false)
        {
            storyManager.isSkip = true;
            skipCoverImage.SetActive(true);
        }
        else
        {
            storyManager.isSkip = false;
            skipCoverImage.SetActive(false);
        }
    }


    public void Save() => SaveManager.instance.SaveGame();

    public void Load()
    {
        SaveManager.instance.LoadGame();
        SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainStoryScene);
    }

    public void Title() => SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainMenu);

}
