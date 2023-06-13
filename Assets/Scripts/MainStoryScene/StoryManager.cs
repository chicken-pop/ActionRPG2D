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

    public StoryData[] storydatas;
    public OptionData optionData;

    [SerializeField] private GameObject[] optionButton;

    [SerializeField] private UI_FadeScreen fadeOut;

    public int storyIndex = -1;
    public int textIndex;

    private bool isTextEnd;

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
        var storyElement = storydatas[_storyIndex].stories[_textIndex];

        image.sprite = storyElement.Sprite;
        nameText.text = storyElement.CharacterName;
        date.text = storydatas[_storyIndex].date;


        StartCoroutine(TypeSentence(_storyIndex, _textIndex));

        AudioManager.Instance.PlayBGM(storydatas[_storyIndex].bgm);



    }

    public void StoryProgression(int _storyIndex)
    {
        if (textIndex < storydatas[_storyIndex].stories.Count)
        {
            SetStoryElement(storyIndex, textIndex);
        }
        else
        {
            storyText.text = storydatas[_storyIndex].stories[textIndex - 1].StoryText;
            textIndex = 0;

            //BattleSceneForestシーンチェンジ
            if (storydatas[_storyIndex].fadeOut == true && GameProgressManager.Instance.flagList.Flags[1].IsOn == false)
            {
                storyIndex++;

                fadeOut.FadeOut();
                StartCoroutine(BattleSceneChangeForest());
                return;
            }

            //選択肢
            if (storydatas[_storyIndex].questionIndex >= 0)
            {
                SetOption(storydatas[_storyIndex].questionIndex);
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

    private IEnumerator BattleSceneChangeForest()
    {
        GameProgressManager.Instance.SetFlag(1); //BeforeBattleForestのフラグを立てる
        yield return new WaitForSeconds(1);
        SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainBattleSceneForest);
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
        Debug.Log(storyIndex);
        Debug.Log(textIndex);
        _data.Story = storyIndex;
        _data.StoryTextIndex = textIndex;
    }
}
