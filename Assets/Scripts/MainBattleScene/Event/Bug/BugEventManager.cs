using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BugEventManager : MonoBehaviour, ISaveManager
{
    public static BugEventManager instance;
    public BugFlagList BugList;

    public int AllBugCount { get; private set; }

    public int BugCount = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        BugList.InitFlags();

        if (SceneManager.GetActiveScene().name == "MainBattleSceneForest")
        {
            AllBugCount = 5;
        }
    }

    public void FindBug() => BugCount++;

    public void LoadData(GameData _data)
    {
        for (int i = 0; i < BugList.BugFlags.Count; i++)
        {
            if (_data.BugEvent.TryGetValue(BugList.BugFlags[i].name, out bool value))
            {
                BugList.BugFlags[i].IsOn = value;
            }
        }

        BugCount = _data.BugCount;
    }

    public void SaveData(ref GameData _data)
    {
        for (int i = 0; i < BugList.BugFlags.Count; i++)
        {
            var BugflagElement = BugList.BugFlags[i];

            if (_data.BugEvent.TryGetValue(BugflagElement.name, out bool value))
            {
                _data.BugEvent.Remove(BugflagElement.name);
                _data.BugEvent.Add(BugflagElement.name, BugflagElement.IsOn);
            }
            else
            {
                _data.BugEvent.Add(BugflagElement.name, BugflagElement.IsOn);
            }
        }

        _data.BugCount = BugCount;
    }
}
