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

        SetStoryElement(storyIndex, textIndex);
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

            //BattleSceneForestシーンチェンジ
            if (storyDatas[_storyIndex].fadeOut == true && GameProgressManager.Instance.flagList.Flags[2].IsOn == false)
            {
                storyIndex++;

                fadeOut.FadeOut();
                StartCoroutine(ForestBattleSceneChange());
                return;
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

    private IEnumerator ForestBattleSceneChange()
    {
        GameProgressManager.Instance.SetFlag(2); //StartForestStoryのフラグを立てる
        yield return new WaitForSeconds(1);
        SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainBattleSceneForest);
    }
}
