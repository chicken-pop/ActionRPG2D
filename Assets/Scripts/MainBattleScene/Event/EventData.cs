using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event Data", menuName = "Data/Event")]
public class EventData : ScriptableObject
{
    public List<Events> Events = new List<Events>();
}

[System.Serializable]
public class Events
{
    [TextArea]
    public string WindowText;
    public Sprite CharacterImage;
    public AudioManager.SE SE;
}
