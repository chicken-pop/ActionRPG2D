using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugWizard : NPCEventTrigger
{
    [SerializeField] private ItemData item;

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        if (BugEventManager.instance.BugList.BugFlags[6].IsOn)
        {
            return;
        }

        Inventory.instance.AddItem(item);

        BugEventManager.instance.FindBug(); //バグのカウントを増やす
        BugEventManager.instance.BugList.BugFlags[6].ChangeFlagStatus();
    }


}
