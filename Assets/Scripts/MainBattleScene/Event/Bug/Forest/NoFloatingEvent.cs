using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFloatingEvent : MonoBehaviour
{
    private EventTrigger eventTrigger => GetComponent<EventTrigger>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStats>().isDead)
            {
                return;
            }

            if (eventTrigger.canEvent == false)
            {
                if (BugEventManager.instance.BugList.BugFlags[1].IsOn)
                {
                    return;
                }

                BugEventManager.instance.FindBug(); //バグのカウントを増やす
                BugEventManager.instance.BugList.BugFlags[1].ChangeFlagStatus();

            }

        }
    }
}
