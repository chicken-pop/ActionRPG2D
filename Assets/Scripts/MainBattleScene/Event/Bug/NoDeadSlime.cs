using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDeadSlime : MonoBehaviour
{
    private EnemyStats slimeStats => GetComponent<EnemyStats>();
    private Event eventData => GetComponent<Event>();

    [SerializeField] private bool canEvent = false;

    private void Update()
    {
        if(slimeStats.currentHealth<= 999800)
        {
            canEvent = true;
            slimeStats.currentHealth = 1000000;
        }

        if (canEvent)
        {
            eventData.SetupEvent(_textIndex: 0);
            //AudioManager.Instance.StopSE(AudioManager.SE.dash); //se消す
            BattleSceneGameManager.instance.PauseGame(true);

            canEvent = false;

            if (BugEventManager.instance.BugList.BugFlags[4].IsOn)
            {
                return;
            }

            BugEventManager.instance.FindBug(); //バグのカウントを増やす
            BugEventManager.instance.BugList.BugFlags[4].ChangeFlagStatus();
        }
    }
}
