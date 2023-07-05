using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDamageTrap : MonoBehaviour
{
    [SerializeField] private int NoDamageCount;

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
                NoDamageCount++;

                if (NoDamageCount >= 1)
                {
                    eventTrigger.canEvent = true;
                }

                if (BugEventManager.instance.BugList.BugFlags[2].IsOn)
                {
                    return;
                }

                if (NoDamageCount == 2)
                {
                    BugEventManager.instance.FindBug(); //バグのカウントを増やす
                    BugEventManager.instance.BugList.BugFlags[2].ChangeFlagStatus();
                }
            }

        }
    }
}
