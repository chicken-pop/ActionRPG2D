using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour , ISaveManager
{
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject inGameUI;

    public UI_Item_Tooltip itemTooltip;
    public UI_StatToolTip statToolTip;
    public UI_CraftWindow craftWindow;
    public UI_SkillToolTip skillToolTip;

    [SerializeField] private UI_VolumeSlider[] volumeSettings;

    private void Awake()
    {
        //スキルツリーにイベントを先に渡すため
        SwitchTo(skillTreeUI);
    }

    private void Start()
    {
        SwitchTo(inGameUI);

        itemTooltip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchWithKeyTo(characterUI);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchWithKeyTo(craftUI);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchWithKeyTo(skillTreeUI);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SwitchWithKeyTo(optionsUI);
        }
    }

    public void SwitchTo(GameObject _menu)
    {
        //Canvas内を全て非表示にして
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        //表示する
        if (_menu != null)
        {
            AudioManager.instance.PlaySE(7, null);
            _menu.SetActive(true);
        }
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        //すでに表示されていたら非表示にする
        if(_menu != null && _menu.activeSelf)
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
                if(item.parameter == pair.Key)
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
}
