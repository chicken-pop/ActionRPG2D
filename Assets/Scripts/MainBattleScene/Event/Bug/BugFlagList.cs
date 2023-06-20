using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BugFlagList", menuName = "Data/FlagList/BugFlagList")]
public class BugFlagList : ScriptableObject
{
    public List<FlagData> BugFlags = new();

    public void InitFlags()
    {
        for (int i = 0; i < BugFlags.Count; i++)
        {
            BugFlags[i].IsOn = false;
        }
    }
}
