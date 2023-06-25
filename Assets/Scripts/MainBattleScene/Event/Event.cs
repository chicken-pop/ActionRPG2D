using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    [SerializeField] private EventData eventData;

    [SerializeField] private GameObject eventUI;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI windowText;

    [SerializeField] Button nextTextButton;

    private int textIndex;
    private bool isTextEnd;

    public bool WizardEvent = false;
    public bool ForestBossEvent = false;

    private void Start()
    {
        windowText.text = "";
        nextTextButton.GetComponent<Button>().onClick.AddListener(() => NextButton());

        if (GetComponentInChildren<WizardParametterUpEvent>())
        {
            WizardEvent = true;
        }

        if (GetComponentInChildren<ForestBossEvent>())
        {
            ForestBossEvent = true;
        }
    }

    public void SetupEvent(int _textIndex)
    {
        eventUI.SetActive(true);
        characterImage.sprite = eventData.Events[_textIndex].CharacterImage;
        StartCoroutine(TypeSentence(_textIndex));
    }

    private void EventProgression()
    {
        if (textIndex < eventData.Events.Count)
        {
            SetupEvent(textIndex);
        }
        else
        {
            textIndex = 0;
            eventUI.SetActive(false);

            if (WizardEvent == true)
            {
                WizardEvent = false;
                return;
            }

            if(ForestBossEvent == true)
            {
                ForestBossEvent = false;
                return;
            }

            BattleSceneGameManager.instance.PauseGame(false);
        }

    }

    private void NextButton()
    {
        if (isTextEnd)
        {
            textIndex++;
            windowText.text = "";
            EventProgression();
            isTextEnd = false;
        }
    }

    public IEnumerator TypeSentence(int _textIndex)
    {
        foreach (char letter in eventData.Events[_textIndex].WindowText.ToCharArray())
        {
            windowText.text += letter;
            yield return null;
        }

        isTextEnd = true;
    }


}
