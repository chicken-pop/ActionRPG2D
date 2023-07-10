using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneStoryManager : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI storyText;

    [SerializeField] private GameObject nextButton;

    [SerializeField] private BattleSceneStoryData[] storyDatas;

    [SerializeField] private UI_FadeScreen fadeOut;

    private int storyIndex;
    private int textIndex;

    private bool isTextEnd;

    private void Start()
    {
        nextButton.GetComponent<Button>().onClick.AddListener(() => NextButton());
        nextButton.SetActive(false);

        storyText.text = "";


        //フラグ判断でスタートするStoryDataを決める
        if (GameProgressManager.Instance.flagList.Flags[(int)GameProgressManager.FlagName.StartForestStory].IsOn == false)
        {
            //Forest0
            storyIndex = 0;
            SetStoryElement(storyIndex, textIndex);
            return;

        }

        if(GameProgressManager.Instance.flagList.Flags[(int)GameProgressManager.FlagName.StartSnowyMountainStory].IsOn == false)
        {
            //SnowyMountain0
            storyIndex = 2;
            SetStoryElement(storyIndex, textIndex);
            return;
        }

        if(GameProgressManager.Instance.flagList.Flags[(int)GameProgressManager.FlagName.AfterSnowyMountainStroy].IsOn == false)
        {
            //SnowyMountain1
            storyIndex = 3;
            SetStoryElement(storyIndex, textIndex);
            return;
        }

        if (GameProgressManager.Instance.flagList.Flags[(int)GameProgressManager.FlagName.StartBackcountryStory].IsOn == false)
        {
            //BeforeBackcountry0
            storyIndex = 4;
            SetStoryElement(storyIndex, textIndex);
            return;
        }

    }

    private void SetStoryElement(int _storyIndex, int _textIndex)
    {
        storyText.text = "";
        var storyElement = storyDatas[_storyIndex].stories[_textIndex];

        background.sprite = storyElement.Background;

        StartCoroutine(TypeSentence(_storyIndex, _textIndex));

        AudioManager.Instance.PlayBGM(storyDatas[_storyIndex].bgm);

    }

    public void StoryProgression(int _storyIndex)
    {
        if (textIndex < storyDatas[_storyIndex].stories.Count)
        {
            SetStoryElement(storyIndex, textIndex);
        }
        else
        {
            storyText.text = storyDatas[_storyIndex].stories[textIndex - 1].StoryText;
            textIndex = 0;

            //BattleSceneにチェンジ
            if (storyDatas[_storyIndex].fadeOut == true)
            {
                fadeOut.FadeOut();

                if(GameProgressManager.Instance.flagList.Flags[(int)GameProgressManager.FlagName.StartForestStory].IsOn == false)
                {
                    StartCoroutine(BattleSceneChange(_flagIndex: (int)GameProgressManager.FlagName.StartForestStory, _sceneName: SceneChangeManager.MainBattleSceneForest));
                    return;
                }

                if(GameProgressManager.Instance.flagList.Flags[(int)GameProgressManager.FlagName.StartSnowyMountainStory].IsOn == false)
                {
                    StartCoroutine(BattleSceneChange(_flagIndex: (int)GameProgressManager.FlagName.StartSnowyMountainStory, _sceneName: SceneChangeManager.MainBattleSceneSnowyMountain));
                    return;
                }

                if (GameProgressManager.Instance.flagList.Flags[(int)GameProgressManager.FlagName.AfterSnowyMountainStroy].IsOn == false)
                {
                    StartCoroutine(BattleSceneChange(_flagIndex: (int)GameProgressManager.FlagName.AfterSnowyMountainStroy, _sceneName: SceneChangeManager.MainStoryScene));
                    return;
                }

                if (GameProgressManager.Instance.flagList.Flags[(int)GameProgressManager.FlagName.StartBackcountryStory].IsOn == false)
                {
                    StartCoroutine(BattleSceneChange(_flagIndex: (int)GameProgressManager.FlagName.StartBackcountryStory, _sceneName: SceneChangeManager.MainStoryScene));
                    return;
                }

            }

            ChangeStoryElement(_storyIndex);

        }
    }

    private void ChangeStoryElement(int _storyIndex)
    {
        storyIndex++;
        SetStoryElement(storyIndex, textIndex);
    }

    private void NextButton()
    {
        if (isTextEnd)
        {        
            textIndex++;
            storyText.text = "";
            StoryProgression(storyIndex);
            isTextEnd = false;
        }

        nextButton.SetActive(false);
    }

    public IEnumerator TypeSentence(int _storyIndex, int _textIndex)
    {
        foreach (char letter in storyDatas[_storyIndex].stories[_textIndex].StoryText.ToCharArray())
        {
            storyText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        isTextEnd = true;
        nextButton.SetActive(true);
    }

    private IEnumerator BattleSceneChange(int _flagIndex, string _sceneName)
    {
        GameProgressManager.Instance.SetFlag(_flagIndex); //StartForestStoryのフラグを立てる
        yield return new WaitForSeconds(1);
        SceneChangeManager.Instance.ChangeScene(_sceneName);
    }
}
