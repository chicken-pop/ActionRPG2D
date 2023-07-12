using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowyMountainBosEvent : BossEvent
{
    protected override void IsMoveRestriction(bool isActive)
    {
        base.IsMoveRestriction(isActive);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        /*
        if (eventData.BossEvent == false)
        {
            BattleSceneGameManager.instance.PauseGame(false);
            IsMoveRestriction(true);
            AudioManager.Instance.PlayBGM(AudioManager.BGM.Boss);
            return;
        }

        if (eventData.BossEvent == true && eventTrigger.canEvent == false)
        {
            IsMoveRestriction(false);
            AudioManager.Instance.PlayBGM(AudioManager.BGM.Battle);

        }
        */

        base.Update();

        if (eventData.BossEvent == false)
        {
            AudioManager.Instance.PlayBGM(AudioManager.BGM.Boss);
            return;
        }


        if (eventData.BossEvent == true && eventTrigger.canEvent == false)
        {
            AudioManager.Instance.PlayBGM(AudioManager.BGM.Battle);
        }
    }
}
