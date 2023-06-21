using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardParametterUpEvent : MonoBehaviour
{
    [SerializeField] private GameObject wizardEvent_UI;

    private void Start()
    {
        Invoke("AddParameter", 0.1f);
    }

    private void AddParameter()
    {
        wizardEvent_UI.GetComponent<UI_WizardEvent>().AddParametter();
    }
}
