using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Option Data", menuName = "Data/Queation")]
public class OptionData : ScriptableObject
{
    public List<Option> options = new List<Option>();
}

[System.Serializable]
public class Option
{
    public string[] OptionText = new string[3];

    public int goodOptionIndex;
    public int normalOptionIndex;
    public int badOptionIndex;

}
