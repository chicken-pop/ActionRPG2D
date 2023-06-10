using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FlagList", menuName = "Data/FlagList")]
public sealed class FlagList : ScriptableObject
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