using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int SkillPoint;

    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentID;

    public SerializableDictionary<string, float> volumeSettings;

    public GameData()
    {
        this.SkillPoint = 0;
        skillTree = new SerializableDictionary<string, bool>();
        inventory = new SerializableDictionary<string, int>();
        equipmentID = new List<string>();

        volumeSettings = new SerializableDictionary<string, float>();
    }

}
