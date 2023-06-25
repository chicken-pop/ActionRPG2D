using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBossEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] moveRestrictions;

    private Event eventData => GetComponentInParent<Event>();
    private EventTrigger eventTrigger => GetComponent<EventTrigger>();

    private void Start()
    {
        IsMoveRestriction(false);
    }

    private void Update()
    {
        if (eventData.ForestBossEvent == false)
        {
            BattleSceneGameManager.instance.PauseGame(false);
            IsMoveRestriction(true);
            AudioManager.Instance.PlayBGM(AudioManager.BGM.Boss);
            return;
        }

        if (eventData.ForestBossEvent == true && eventTrigger.canEvent == false)
        {
            IsMoveRestriction(false);

            AudioManager.Instance.StopBGM();

        }
    }

    private void IsMoveRestriction(bool isActive)
    {
        for (int i = 0; i < moveRestrictions.Length; i++)
        {
            moveRestrictions[i].SetActive(isActive);
        }
    }
}
