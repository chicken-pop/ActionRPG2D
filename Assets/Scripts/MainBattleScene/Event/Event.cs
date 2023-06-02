using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    [SerializeField] private BugEventData bugEventData;

    [SerializeField] private GameObject eventUI;
    [SerializeField] private TextMeshProUGUI windowText;

    [SerializeField] Button nextTextButton;

    private int textIndex;
    private bool isTextEnd;

    private void Start()
    {
        windowText.text = "";
        nextTextButton.GetComponent<Button>().onClick.AddListener(() => NextButton());
    }

    public void SetupEvent(int _textIndex)
    {
        eventUI.SetActive(true);
        StartCoroutine(TypeSentence(_textIndex));
    }

    private void BugStoryProgression()
    {
        if (textIndex < bugEventData.BugEvents.Count)
        {
            SetupEvent(textIndex);
        }
        else
        {
            eventUI.SetActive(false);
            BattleSceneGameManager.instance.PauseGame(false);
        }

    }

    private void NextButton()
    {
        if (isTextEnd)
        {
            textIndex++;
            windowText.text = "";
            BugStoryProgression();
            isTextEnd = false;
        }
    }

    public IEnumerator TypeSentence(int _textIndex)
    {
        foreach (char letter in bugEventData.BugEvents[_textIndex].WindowText.ToCharArray())
        {
            windowText.text += letter;
            yield return null;
        }

        isTextEnd = true;
    }


}
