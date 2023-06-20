using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardParametterUpEvent : MonoBehaviour
{
    [SerializeField] private GameObject wizardEvent_UI;
    [SerializeField] private GameObject inGame_UI;

    private Event eventData => GetComponentInParent<Event>();
    private EventTrigger eventTrigger => GetComponent<EventTrigger>();

    private void Start()
    {
        Invoke("AddParameter", 0.1f);
    }

    private void Update()
    {
        if (eventData.wizardEvent == false)
        {
            inGame_UI.SetActive(false);
            wizardEvent_UI.SetActive(true);
        }
        else
        {
            eventTrigger.canEvent = true;
        }
        
    }

    private void AddParameter()
    {
        wizardEvent_UI.GetComponent<UI_ParametterUp>().AddParametter();
    }
}
