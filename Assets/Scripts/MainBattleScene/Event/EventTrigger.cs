using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTrigger : MonoBehaviour
{
    private Event eventData => GetComponentInParent<Event>();

    public bool canEvent = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStats>().isDead)
            {
                return;
            }

            if (canEvent)
            {
                eventData.SetupEvent(0);
                AudioManager.Instance.StopSE(AudioManager.SE.dash); //se����
                GameManager.instance.PauseGame(true);
            }

            canEvent = false;

        }
    }
}
