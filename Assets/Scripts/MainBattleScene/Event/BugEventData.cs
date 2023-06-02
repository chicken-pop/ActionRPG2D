using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bug Event Data", menuName = "Data/BugEvent")]
public class BugEventData : ScriptableObject
{
    public List<BugEvent> BugEvents = new List<BugEvent>();
}

[System.Serializable]
public class BugEvent
{
    [TextArea]
    public string WindowText;
}
