using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBossEvent : BossEvent
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (eventData.BossEvent == false)
        {
            AudioManager.Instance.PlayBGM(AudioManager.BGM.Boss);
            return;
        }


        if (eventData.BossEvent == true && eventTrigger.canEvent == false)
        {
            AudioManager.Instance.StopBGM();
        }
    }

    protected override void IsMoveRestriction(bool isActive)
    {
        base.IsMoveRestriction(isActive);
    }
}
