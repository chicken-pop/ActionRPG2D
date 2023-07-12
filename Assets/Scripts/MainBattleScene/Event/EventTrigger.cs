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

            StartCoroutine(StartEvent());

            /*
            if (eventData.BossEvent)
            {
                Debug.Log("a");
                BattleSceneCameraMamager.Instance.BossEventCameraSetting();
            }

            if (canEvent)
            {
                eventData.SetupEvent(_textIndex: 0);
                BattleSceneGameManager.instance.PauseGame(true);
            }

            canEvent = false;

            */


        }
    }

    private IEnumerator StartEvent()
    {
        if (eventData.BossEvent)
        {
            PlayerManager.instance.player.isAction = false;
            BattleSceneCameraMamager.Instance.BossEventCameraSetting();
            yield return new WaitForSeconds(2f);
        }

        if (canEvent)
        {
            eventData.SetupEvent(_textIndex: 0);
            BattleSceneGameManager.instance.PauseGame(true);
        }

        canEvent = false;
    }
}
