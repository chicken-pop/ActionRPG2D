using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int SkillPoint;

    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentID;

    public GameData()
    {
        this.SkillPoint = 0;
        inventory = new SerializableDictionary<string, int>();
        equipmentID = new List<string>();
    }

}
