using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button nextText;

    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private StoryData[] storydatas;

    private int storyIndex;
    private int textIndex;

    private bool isTextEnd;

    private void Start()
    {
        storyText.text = "";
        nameText.text = "";

        SetStoryElement(storyIndex, textIndex);

        nextText.GetComponent<Button>().onClick.AddListener(() => NextButton());
    }



    private void SetStoryElement(int _storyIndex, int _textIndex)
    {
        var storyElement = storydatas[_storyIndex].stories[_textIndex];

        image.sprite = storyElement.Sprite;
        nameText.text = storyElement.CharacterName;

        StartCoroutine(TypeSentence(_storyIndex, _textIndex));

        if (_textIndex == 0)
        {
            AudioManager.Instance.PlayBGM(storydatas[_storyIndex].bgm);
        }

    }

    private void StoryProgression(int _storyIndex)
    {
        if(textIndex < storydatas[_storyIndex].stories.Count)
        {
            SetStoryElement(storyIndex, textIndex);
        }
        else
        {
            textIndex = 0;

            if (_storyIndex == 0)
            {
                storyIndex = 1;
                SetStoryElement(storyIndex, textIndex);
            }
        }
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
    }

    public IEnumerator TypeSentence(int _storyIndex, int _textIndex)
    {
        foreach (char letter in storydatas[_storyIndex].stories[_textIndex].StoryText.ToCharArray())
        {
            storyText.text += letter;
            yield return null;
        }

        isTextEnd = true;
    }
}
