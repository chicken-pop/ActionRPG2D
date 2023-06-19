using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySceneUI_Menu : MonoBehaviour, ISaveManager
{
    public Button button;

    [SerializeField] private UI_VolumeSlider[] volumeSettings;
    [SerializeField] private GameObject menu;

    private void Start()
    {
        button.onClick.AddListener(SetMenu);
        menu.SetActive(false);
        Debug.Log("c");
    }

    private void SetMenu()
    {
        if (menu.activeSelf == true)
        {
            menu.SetActive(false);
            return;
        }

        menu.SetActive(true);
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
}
