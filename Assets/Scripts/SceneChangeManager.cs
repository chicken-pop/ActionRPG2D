using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : SingletonMonoBehaviour<SceneChangeManager>
{
    public const string MainStoryScene = "MainStoryScene";
    public const string BattleSceneStory = "BattleSceneStory";
    public const string MainBattleSceneForest = "MainBattleSceneForest";
    public const string MainBattleSceneSnowyMountain = "MainBattleSceneSnowyMountain";

    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

}
