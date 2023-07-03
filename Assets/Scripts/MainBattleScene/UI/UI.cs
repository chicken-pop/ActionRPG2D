using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour, ISaveManager
{
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject restartButton; 
    [Space]

    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject eventUI;
    [SerializeField] private GameObject wizardEventUI;

    public UI_Item_Tooltip itemTooltip;
    public UI_StatToolTip[] statToolTip;
    public UI_CraftWindow craftWindow;
    public UI_SkillToolTip skillToolTip;

    [SerializeField] private UI_VolumeSlider[] volumeSettings;

    [Header("Wizard event info")]
    [SerializeField] private Event eventData;
    [SerializeField] private NPCEventTrigger eventTrigger;
    public bool isWizardEventUI = false;

    private void Awake()
    {

        //UI_VolueSliderのAwakeを呼ぶため
        SwitchTo(optionsUI);
        //スキルツリーにイベントを先に渡すため
        skillTreeUI.SetActive(true);
        //AddParametter()を呼ぶため
        wizardEventUI.SetActive(true);

    }

    private void Start()
    {
        SwitchTo(inGameUI);

        itemTooltip.gameObject.SetActive(false);
        statToolTip[0].gameObject.SetActive(false);
        statToolTip[1].gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isWizardEventUI == false)
        {
            SwitchWithKeyTo(characterUI);
        }

        if (Input.GetKeyDown(KeyCode.B) && isWizardEventUI == false)
        {
            SwitchWithKeyTo(craftUI);
        }

        if (Input.GetKeyDown(KeyCode.K) && isWizardEventUI == false)
        {
            SwitchWithKeyTo(skillTreeUI);
        }

        if (Input.GetKeyDown(KeyCode.O) && isWizardEventUI == false)
        {
            SwitchWithKeyTo(optionsUI);
        }

        if(eventData.WizardEvent == false)
        {
            eventData.WizardEvent = true;

            isWizardEventUI = true;
            SwitchWithKeyTo(wizardEventUI);
        }
    }

    public void SwitchTo(GameObject _menu)
    {

        //Canvas内を全て非表示にして
        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;

            if (fadeScreen == false)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        //表示する
        if (_menu != null)
        {
            AudioManager.Instance.PlaySE(AudioManager.SE.changeUI, null);
            _menu.SetActive(true);
        }

        if (BattleSceneGameManager.instance != null)
        {
            if (_menu == inGameUI)
            {
                BattleSceneGameManager.instance.PauseGame(false);
                AudioManager.Instance.StopSE(AudioManager.SE.changeUI);
            }
            else
            {
                //UI画面時の時間を止める
                BattleSceneGameManager.instance.PauseGame(true);
            }
        }
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        //すでに表示されていたら非表示にする
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return;
            }

            SwitchTo(inGameUI);
        }
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, float> pair in _data.volumeSettings)
        {
            foreach (UI_VolumeSlider item in volumeSettings)
            {
                if (item.parameter == pair.Key)
                {
                    item.LoadSlider(pair.Value);
                }
            }
        }

    }

    public void SaveData(ref GameData _data)
    {
        _data.volumeSettings.Clear();

        foreach (UI_VolumeSlider item in volumeSettings)
        {
            _data.volumeSettings.Add(item.parameter, item.slider.value);
        }
    }

    public void SwitchOnEndScreen()
    {
        fadeScreen.FadeOut();
        StartCoroutine(EndScreenCoroutine());
    }

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        eventUI.SetActive(true);
        eventUI.GetComponentInChildren<TextMeshProUGUI>().text = "バグ多すぎ…";

        yield return new WaitForSeconds(1);
        restartButton.SetActive(true);
        //restartButton.GetComponent<Button>().onClick.AddListener(() =>BattleSceneGameManager.instance.RestartScene());
        
    }

    public void RestartGameButton() => BattleSceneGameManager.instance.RestartScene();
}
