using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSeeingWall : MonoBehaviour
{
    private EventTrigger eventTrigger => GetComponentInChildren<EventTrigger>();

    [SerializeField] private int collisionCount; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if (collision.gameObject.GetComponent<CharacterStats>().isDead)
            {
                return;
            }

            if (eventTrigger.canEvent == false)
            {
                collisionCount++;

                if (collisionCount >= 1)
                {
                    eventTrigger.canEvent = true;
                }

                if (BugEventManager.instance.BugList.BugFlags[3].IsOn)
                {
                    return;
                }

                if (collisionCount == 2)
                {
                    BugEventManager.instance.FindBug(); //バグのカウントを増やす
                    BugEventManager.instance.BugList.BugFlags[3].ChangeFlagStatus();
                }
            }
        }
    }
}

