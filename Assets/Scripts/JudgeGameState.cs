using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeGameState : MonoBehaviour
{
    [SerializeField] private int flagCount = 0;

    private void Awake()
    {
        CheckFlags();
    }

    private void Start()
    {
        //Invoke("CheckFlags", 0.1f); //ScriptExecution Order‚Ì‡”Ô‚Í³‚µ‚¢‚Í‚¸‚¾‚ª‚¤‚Ü‚­‚¢‚©‚È‚¢A‚Æ‚è‚ ‚¦‚¸‘ÅŠJô
    }

    private void CheckFlags()
    {
        for (int i = 0; i < GameProgressManager.Instance.flagList.Flags.Count; i++)
        {
            Debug.Log(GameProgressManager.Instance.flagList.Flags[i].IsOn);
            if (GameProgressManager.Instance.flagList.Flags[i].IsOn == true)
            {
                flagCount++;
            }
        }
    }

    public void ContinueGameStart()
    {
        switch (flagCount)
        {
            case 0:
            case 1:
            case 4:
            case 7:
                SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainStoryScene);
                break;
            case 2:
            case 5:
                SceneChangeManager.Instance.ChangeScene(SceneChangeManager.BattleSceneStory);
                break;
            case 3:
                SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainBattleSceneForest);
                break;
            case 6:
                SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainBattleSceneSnowyMountain);
                break;

        }
    }

    public void NewGameStart()
    {
        SaveManager.instance.DeleteSaveData();
        SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainStoryScene);
    }
}
