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
    public bool BossEvent = false;

    private enum EventFlags
    {
        Invalid = -1,
        ForestClear,
        SnowyMountainClear
    }

    [SerializeField] private EventFlags eventFlag = EventFlags.Invalid;

    private void Start()
    {
        windowText.text = "";
        nextTextButton.GetComponent<Button>().onClick.AddListener(() => NextButton());


        if (GetComponentInChildren<WizardParametterUpEvent>())
        {
            WizardEvent = true;
        }

        if (GetComponentInChildren<BossEvent>())
        {
            BossEvent = true;
        }
    }

    public void SetupEvent(int _textIndex)
    {
        eventUI.SetActive(true);
        characterImage.sprite = eventData.Events[_textIndex].CharacterImage;
        AudioManager.Instance.PlaySE(eventData.Events[_textIndex].SE, null);
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

            if (BossEvent == true)
            {
                BossEvent = false;
                return;
            }

            switch (eventFlag)
            {
                case EventFlags.Invalid:
                    break;
                case EventFlags.ForestClear:
                    BattleSceneGameManager.instance.PauseGame(true);
                    StartCoroutine(StageClear(3, SceneChangeManager.MainStoryScene));
                    break;
                case EventFlags.SnowyMountainClear:
                    BattleSceneGameManager.instance.PauseGame(true);
                    StartCoroutine(StageClear(6, SceneChangeManager.BattleSceneStory));
                    break;

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

    private IEnumerator StageClear(int flagIndex, string sceneName)
    {
        yield return new WaitForSeconds(0.3f);
        AudioManager.Instance.StopBGM();
        GameProgressManager.Instance.SetFlag(flagIndex);
        SceneChangeManager.Instance.ChangeScene(sceneName);
    }


}
