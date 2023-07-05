using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPickupItem : MonoBehaviour
{
    [SerializeField] private int pickupAmount;
    [SerializeField] protected int bugFlag;

    private EventTrigger eventTrigger => GetComponent<EventTrigger>();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStats>().isDead)
            {
                return;
            }

            if (eventTrigger.canEvent == false)
            {
                pickupAmount++;

                if (pickupAmount >= 1)
                {
                    eventTrigger.canEvent = true;
                }

                if (BugEventManager.instance.BugList.BugFlags[0].IsOn)
                {
                    return;
                }

                if (pickupAmount == 2)
                {
                    BugEventManager.instance.FindBug(); //バグのカウントを増やす
                    BugEventManager.instance.BugList.BugFlags[bugFlag].ChangeFlagStatus();
                }
            }

        }
    }
}
