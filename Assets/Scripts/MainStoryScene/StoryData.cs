using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story Data", menuName = "Data/Story")]
public class StoryData : ScriptableObject
{
    public List<Story> stories = new List<Story>();

    public AudioManager.BGM bgm;
    public string date;

    public bool fadeOut;
}

[System.Serializable]
public class Story
{
    public Sprite Sprite;

    [TextArea]
    public string StoryText;
    public string CharacterName;

}
