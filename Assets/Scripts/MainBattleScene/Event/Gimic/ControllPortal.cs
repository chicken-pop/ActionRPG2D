using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPortal : MonoBehaviour
{
    [SerializeField] private GameObject[] portal;

    private bool redPortal;
    private bool bluePortal;
    private bool greenPortal;

    private bool stopPortal = false;

    private void Start()
    {
        SetPortal();
    }

    private void SetPortal()
    {
        for (int i = 0; i < portal.Length; i++)
        {
            portal[i].SetActive(false);
        }

        stopPortal = false;
        InvokeRepeating("TriggerPortal", 0, 10);
    }

    private void Update()
    {

        redPortal = portal[0].GetComponent<PortalGimic>().isPortal;
        bluePortal = portal[1].GetComponent<PortalGimic>().isPortal;
        greenPortal = portal[2].GetComponent<PortalGimic>().isPortal;

        if (stopPortal == false)
        {
            if (redPortal == true)
                StartCoroutine(StopPortal(0));
            else if (bluePortal == true)
                StartCoroutine(StopPortal(1));
            else if (greenPortal == true)
                StartCoroutine(StopPortal(2));

        }
    }

    private void TriggerPortal() => StartCoroutine(ChangePortal());

    private IEnumerator ChangePortal()
    {
        portal[0].SetActive(true);
        yield return new WaitForSeconds(3f);

        if (redPortal == true)
            yield break;

        portal[0].SetActive(false);

        portal[1].SetActive(true);
        yield return new WaitForSeconds(3f);

        if (bluePortal == true)
            yield break;

        portal[1].SetActive(false);

        portal[2].SetActive(true);
        yield return new WaitForSeconds(3f);

        if (greenPortal == true)
            yield break;

        portal[2].SetActive(false);
    }

    private IEnumerator StopPortal(int portalIndex)
    {
        stopPortal = true;
        CancelInvoke();
        yield return new WaitForSeconds(5f);
        portal[portalIndex].GetComponent<PortalGimic>().isPortal = false;
        SetPortal();
    }

}
