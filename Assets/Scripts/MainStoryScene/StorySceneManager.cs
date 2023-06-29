using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySceneManager : MonoBehaviour , ISaveManager
{
    public static StorySceneManager instance;
    private int impressionPoint = 0;

    public int ImpressionPoint
    {
        get => impressionPoint;
        set
        {
            impressionPoint = value;

            if (impressionPoint <= 0)
            {
                impressionPoint = 0;
            }
        }
    }

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

    public void LoadData(GameData _data)
    {
        ImpressionPoint = _data.ImpressionPoint;
    }

    public void SaveData(ref GameData _data)
    {
        _data.ImpressionPoint = ImpressionPoint;
    }
}
