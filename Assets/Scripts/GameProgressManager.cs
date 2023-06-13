using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressManager : SingletonMonoBehaviour<GameProgressManager>, ISaveManager
{
    public GameProgressionFlagList flagList;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            flagList.InitFlags();
        }
    }

    //フラグをたててセーブ
    public void SetFlag(int _flagIndex)
    {
        GameProgressManager.Instance.flagList.Flags[_flagIndex].ChangeFlagStatus(); 
        SaveManager.instance.SaveGame();
    }

    public void LoadData(GameData _data)
    {
        for (int i = 0; i < flagList.Flags.Count; i++)
        {
            if (_data.GameFlags.TryGetValue(flagList.Flags[i].name, out bool value))
            {
                flagList.Flags[i].IsOn = value;
            }
        }
    }

    public void SaveData(ref GameData _data)
    {

        for (int i = 0; i < flagList.Flags.Count; i++)
        {
            var flagElement = flagList.Flags[i];

            if (_data.GameFlags.TryGetValue(flagElement.name, out bool value))
            {
                _data.GameFlags.Remove(flagElement.name);
                _data.GameFlags.Add(flagElement.name, flagElement.IsOn);
            }
            else
            {
                _data.GameFlags.Add(flagElement.name, flagElement.IsOn);
            }
        }

    }
}
