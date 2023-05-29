using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story Data", menuName = "Data/Story")]
public class StoryData : ScriptableObject
{
    public List<Story> stories = new List<Story>();
}

[System.Serializable]
public class Story
{
    public Sprite Sprite;

    [TextArea]
    public string StoryText;
    public string CharacterName;

}