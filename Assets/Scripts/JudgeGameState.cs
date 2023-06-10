using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeGameState : MonoBehaviour
{
    [SerializeField] private int flagCount = 0;

    private void Start()
    {
        for (int i = 0; i < GameProgressManager.Instance.flagList.Flags.Count; i++)
        {
            if (GameProgressManager.Instance.flagList.Flags[i].IsOn == true)
            {
                flagCount++;
            }
        }
    }


    public void GameStart()
    {
        switch (flagCount)
        {
            case 0:
                SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainStoryScene);
                break;
            case 1:
                SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainBattleScene);
                break;

        }
    }
}
