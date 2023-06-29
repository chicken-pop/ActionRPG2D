using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour, ISaveManager
{
    [SerializeField] private Image image;
    [SerializeField] private Button nextText;

    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI date;

    [SerializeField] private GameObject OP;

    public StoryData[] storyDatas;
    public StoryData[] answerDatas;
    public OptionData optionData;

    [SerializeField] private GameObject[] optionButton;

    [SerializeField] private UI_FadeScreen fadeOut;

    public int storyIndex = -1;
    public int answerStoryIndex = 0;
    public int textIndex;

    private bool isTextEnd = false;
    public bool isAnswerTurn = false;

    private void Start()
    {
        nextText.GetComponent<Button>().onClick.AddListener(() => NextButton());

        if (storyIndex == -1)
        {
            OP.SetActive(true);
            return;
        }

        storyText.text = "";
        nameText.text = "";
        date.text = "";

        SetStoryElement(storyIndex, textIndex);

    }

    public void SetStoryElement(int _storyIndex, int _textIndex)
    {
        storyText.text = "";
        var storyElement = storyDatas[_storyIndex].stories[_textIndex];

        image.sprite = storyElement.Sprite;
        nameText.text = storyElement.CharacterName;
        date.text = storyDatas[_storyIndex].date;


        StartCoroutine(TypeSentence(_storyIndex, _textIndex, storyDatas));

        AudioManager.Instance.PlayBGM(storyDatas[_storyIndex].bgm);

    }

    public void SetAnswerElement(int _storyIndex, int _textIndex)
    {
        storyText.text = "";
        var storyElement = answerDatas[_storyIndex].stories[_textIndex];

        image.sprite = storyElement.Sprite;
        nameText.text = storyElement.CharacterName;
        date.text = answerDatas[_storyIndex].date;


        StartCoroutine(TypeSentence(_storyIndex, _textIndex, answerDatas));

        AudioManager.Instance.PlayBGM(answerDatas[_storyIndex].bgm);

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

            //BattleSceneÇ…ïœçX
            if (storyDatas[_storyIndex].fadeOut == true)
            {
                storyIndex++;
                fadeOut.FadeOut();

                if(GameProgressManager.Instance.flagList.Flags[1].IsOn == false)
                {
                    StartCoroutine(BattleStorySceneChange(1));
                    return;
                }

                if(GameProgressManager.Instance.flagList.Flags[4].IsOn == false)
                {
                    StartCoroutine(BattleStorySceneChange(4));
                    return;
                }       
            }

            //ëIëéà
            if (storyDatas[_storyIndex].questionIndex >= 0)
            {
                SetOption(storyDatas[_storyIndex].questionIndex);
                return;
            }

            //ì˙Ç‚èÍñ Ç™ïœÇÌÇÈÇ∆Ç´ÇÃââèo

            ChangeStoryElement(_storyIndex);

        }
    }

    public void AnswerStoryProgression(int _storyIndex)
    {
        if (textIndex < answerDatas[_storyIndex].stories.Count)
        {
            SetAnswerElement(_storyIndex, textIndex);
        }
        else
        {
            textIndex = 0;
            isAnswerTurn = false;
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

            if (isAnswerTurn)
            {
                AnswerStoryProgression(answerStoryIndex);
            }
            else if (!isAnswerTurn)
            {
                StoryProgression(storyIndex);
            }

            isTextEnd = false;
        }

    }

    public IEnumerator TypeSentence(int _storyIndex, int _textIndex, StoryData[] data)
    {
        foreach (char letter in data[_storyIndex].stories[_textIndex].StoryText.ToCharArray())
        {
            storyText.text += letter;
            yield return null;
        }

        isTextEnd = true;
    }

    private IEnumerator BattleStorySceneChange(int _flagIndex)
    {
        GameProgressManager.Instance.SetFlag(_flagIndex);
        yield return new WaitForSeconds(1);
        SceneChangeManager.Instance.ChangeScene(SceneChangeManager.BattleSceneStory);
    }

    private void SetOption(int _optionNumber)
    {
        for (int i = 0; i < optionButton.Length; i++)
        {
            optionButton[i].SetActive(true);
            optionButton[i].GetComponentInChildren<TextMeshProUGUI>().text = optionData.options[_optionNumber].OptionText[i];
        }
    }

    public void LoadData(GameData _data)
    {
        storyIndex = _data.Story;
        textIndex = _data.StoryTextIndex;
    }

    public void SaveData(ref GameData _data)
    {
        _data.Story = storyIndex;
        _data.StoryTextIndex = textIndex;
    }
}
