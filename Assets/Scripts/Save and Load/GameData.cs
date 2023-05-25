using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int SkillPoint;

    public SerializableDictionary<string, int> inventory;

    public GameData()
    {
        this.SkillPoint = 0;
        inventory = new SerializableDictionary<string, int>();
    }

}
