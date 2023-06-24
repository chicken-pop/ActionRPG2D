using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story Data", menuName = "Data/BattleStory")]
public class BattleSceneStoryData : ScriptableObject
{
    public List<BattleStory> stories = new List<BattleStory>();

    public AudioManager.BGM bgm;
    public bool fadeOut;

}

[System.Serializable]
public class BattleStory
{
    public Sprite Background;

    [TextArea]
    public string StoryText;

}
