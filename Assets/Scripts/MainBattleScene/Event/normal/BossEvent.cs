using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] moveRestrictions;

    protected Event eventData => GetComponentInParent<Event>();
    protected EventTrigger eventTrigger => GetComponent<EventTrigger>();

    protected virtual void Start()
    {
        IsMoveRestriction(false);
    }

    protected virtual void Update()
    {
        if (eventData.BossEvent == false)
        {
            BattleSceneGameManager.instance.PauseGame(false);
            IsMoveRestriction(true);
            AudioManager.Instance.PlayBGM(AudioManager.BGM.Boss);
            return;
        }

        if (eventData.BossEvent == true && eventTrigger.canEvent == false)
        {
            IsMoveRestriction(false);

            AudioManager.Instance.StopBGM();

        }
    }

    protected virtual void IsMoveRestriction(bool isActive)
    {
        for (int i = 0; i < moveRestrictions.Length; i++)
        {
            moveRestrictions[i].SetActive(isActive);
        }
    }
}
