using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySceneUI_OptionButton : MonoBehaviour
{
    [SerializeField] private StoryManager storyManager;
    [SerializeField] private int buttonIndex;

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
                    SetStory(1);
                else if (buttonIndex == normalOptionIndex)
                    SetStory(2);
                else if (buttonIndex == normalOptionIndex)
                    SetStory(3);
                break;

        }
    }

    private void SetStory(int _nextStoryIndex)
    {
        storyManager.storyIndex = _nextStoryIndex;
        storyManager.SetStoryElement(storyManager.storyIndex, storyManager.textIndex);
    }
}
