using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySceneManager : MonoBehaviour , ISaveManager
{
    public static StorySceneManager instance;
    public int ImpressionPoint = 0;

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

    public void AddImpressionPoint()
    {
        ImpressionPoint += 5;
    }

    public void DecreaseImpressionPoint()
    {
        ImpressionPoint += -3;

        if (ImpressionPoint <= 0)
        {
            ImpressionPoint = 0;
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
