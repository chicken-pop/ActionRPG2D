using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySceneUI_OptionButton : MonoBehaviour
{
    [SerializeField] private StoryManager storyManager;
    [SerializeField] private int buttonIndex;

    [SerializeField] private GameObject goodImpression;
    [SerializeField] private GameObject normalImpression;
    [SerializeField] private GameObject badImpression;

    private int storyIndex;

    private int goodOptionIndex;
    private int normalOptionIndex;
    private int badOptionIndex;

    public Button optionButton;

    [SerializeField] private GameObject[] buttons;

    private void Start()
    {
        optionButton.GetComponent<Button>().onClick.AddListener(ProgressioinStory);
    }

    private void Update()
    {
        //SetStory();
    }

    private void ProgressioinStory()
    {
        storyIndex = storyManager.storyIndex;
        var qustionIndex = storyManager.optionData.options[storyManager.storyDatas[storyIndex].questionIndex];

        goodOptionIndex = qustionIndex.goodOptionIndex;
        normalOptionIndex = qustionIndex.normalOptionIndex;
        badOptionIndex = qustionIndex.badOptionIndex;


        JudgeQuestionIndex();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }

    }

    private void JudgeQuestionIndex()
    {
        switch (storyManager.storyDatas[storyIndex].questionIndex)
        {
            //質問のIndexとその回答に対するStoryDataをいれる
            case 0:
                if (buttonIndex == goodOptionIndex)
                    SetStory(_answerStoryIndex: 0, _impression: 0);
                else if (buttonIndex == normalOptionIndex)
                    SetStory(_answerStoryIndex: 1, _impression: 1);
                else if (buttonIndex == badOptionIndex)
                    SetStory(_answerStoryIndex: 2, _impression: 2);
                break;

        }
    }

    private void SetStory(int _answerStoryIndex,int _impression)
    {
        storyManager.isAnswerTurn = true;
        storyManager.answerStoryIndex = _answerStoryIndex;
        storyManager.SetAnswerElement(storyManager.answerStoryIndex, storyManager.textIndex);

        if(_impression == 0)
        {
            //好感度Up
            goodImpression.SetActive(true);
            ChangeImpressionPoint(5);

        }
        else if(_impression == 1)
        {
            //好感度Down
            normalImpression.SetActive(true);
            ChangeImpressionPoint(3);
        }
        else if(_impression == 2)
        {
            //好感度Down
            badImpression.SetActive(true);
            ChangeImpressionPoint(-4);
        }
    }

    public void ChangeImpressionPoint(int _impressionPoint) => StorySceneManager.instance.ImpressionPoint += _impressionPoint;
}
