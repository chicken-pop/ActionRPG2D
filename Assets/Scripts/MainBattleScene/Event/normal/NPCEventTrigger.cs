using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEventTrigger : MonoBehaviour
{
    private Event eventData => GetComponentInParent<Event>();

    public bool canEvent = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canEvent = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            canEvent = false;
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStats>().isDead)
            {
                return;
            }

            if (canEvent)
            {
                eventData.SetupEvent(_textIndex: 0);
                //AudioManager.Instance.StopSE(AudioManager.SE.dash); //seè¡Ç∑
                BattleSceneGameManager.instance.PauseGame(true);
            }

            canEvent = false;

        }
    }

}
