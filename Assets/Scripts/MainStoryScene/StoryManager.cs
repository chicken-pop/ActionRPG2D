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
    [SerializeField] private TextMeshProUGUI date;

    public StoryData[] storydatas;
    public OptionData optionData;

    [SerializeField] private GameObject[] optionButton;

    [SerializeField] private UI_FadeScreen fadeOut;

    public int storyIndex;
    public int textIndex;

    private bool isTextEnd;

    private void Start()
    {
        storyText.text = "";
        nameText.text = "";
        date.text = "";

        SetStoryElement(storyIndex, textIndex);

        nextText.GetComponent<Button>().onClick.AddListener(() => NextButton());
    }



    public void SetStoryElement(int _storyIndex, int _textIndex)
    {
        storyText.text = "";
        var storyElement = storydatas[_storyIndex].stories[_textIndex];

        image.sprite = storyElement.Sprite;
        nameText.text = storyElement.CharacterName;
        date.text = storydatas[_storyIndex].date;


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
            storyText.text = storydatas[_storyIndex].stories[textIndex - 1].StoryText;
            textIndex = 0;

            //シーンチェンジ
            if (storydatas[_storyIndex].fadeOut == true)
            {
                fadeOut.FadeOut();
                StartCoroutine(BattleSceneChange()); //フラグ管理して、メソッド化
            }

            //選択肢
            if(storydatas[_storyIndex].questionIndex >= 0)
            {
                SetOption(storydatas[_storyIndex].questionIndex);
            }

            /*演出確認のため、一旦消す
            textIndex = 0;

            if (_storyIndex == 0)
            {
                storyIndex = 1;
                SetStoryElement(storyIndex, textIndex);
            }
            */
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

    private IEnumerator BattleSceneChange()
    {
        yield return new WaitForSeconds(1);
        SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainBattleScene);
    }

    private void SetOption(int _optionNumber)
    {
        for (int i = 0; i < optionButton.Length; i++)
        {
            optionButton[i].SetActive(true);
            optionButton[i].GetComponentInChildren<TextMeshProUGUI>().text = optionData.options[_optionNumber].OptionText[i];
        }
    }
}
