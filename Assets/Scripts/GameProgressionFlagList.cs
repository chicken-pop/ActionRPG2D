using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FlagList", menuName = "Data/FlagList/GameProgressionFlagList")]
public class GameProgressionFlagList : ScriptableObject
{
    public List<FlagData> Flags = new();

    public void InitFlags()
    {
        for (int i = 0; i < Flags.Count; i++)
        {
            Flags[i].IsOn = false;
        }
    }

}