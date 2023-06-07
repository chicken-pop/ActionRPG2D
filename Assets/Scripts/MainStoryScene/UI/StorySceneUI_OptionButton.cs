using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySceneUI_OptionButton : MonoBehaviour
{
    [SerializeField] private StoryManager storyManager;
    [SerializeField] private int buttonIndex;

    [SerializeField] private GameObject goodImpression;
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
        var qustionIndex = storyManager.optionData.options[storyManager.storydatas[storyIndex].questionIndex];

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
        switch (storyManager.storydatas[storyIndex].questionIndex)
        {
            //Ž¿–â‚ÌIndex‚Æ‚»‚Ì‰ñ“š‚É‘Î‚·‚éStoryData‚ð‚¢‚ê‚é
            case 0:
                if (buttonIndex == goodOptionIndex)
                    SetStory(_nextStoryIndex: 1, _impression: 0);
                else if (buttonIndex == normalOptionIndex)
                    SetStory(_nextStoryIndex: 2, _impression: 2);
                else if (buttonIndex == normalOptionIndex)
                    SetStory(_nextStoryIndex: 3, _impression: 1);
                break;

        }
    }

    private void SetStory(int _nextStoryIndex,int _impression)
    {
        storyManager.storyIndex = _nextStoryIndex;
        storyManager.SetStoryElement(storyManager.storyIndex, storyManager.textIndex);

        if(_impression == 0)
        {
            goodImpression.SetActive(true);
        }
        else if(_impression == 1)
        {
            badImpression.SetActive(false);
        }
        else
        {
            return;
        }
    }
}
