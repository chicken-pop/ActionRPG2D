using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeBattleScene : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;

    private void Update()
    {
        //‰¼
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneChangeManager.Instance.ChangeScene(SceneChangeManager.MainBattleSceneForest);
            saveManager.SaveGame();
        }
    }
}
