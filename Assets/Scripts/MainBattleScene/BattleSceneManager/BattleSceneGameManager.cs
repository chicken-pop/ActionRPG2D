using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneGameManager : MonoBehaviour
{
    public static BattleSceneGameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void RestartScene()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case SceneChangeManager.MainBattleSceneForest:
                SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainBattleSceneForest);
                break;
            case SceneChangeManager.MainBattleSceneSnowyMountain:
                SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainBattleSceneSnowyMountain);
                break;

        }
    }

    public void PauseGame(bool _pause)
    {
        if (_pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
