using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPickupItem : MonoBehaviour
{
    [SerializeField] private int pickupAmount;

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
                pickupAmount++;

                if (pickupAmount >= 2)
                {
                    eventTrigger.canEvent = true;
                }
            }

        }
    }
}
