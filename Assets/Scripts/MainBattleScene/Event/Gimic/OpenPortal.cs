using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPortal : MonoBehaviour
{
    [SerializeField] private GameObject[] portals;
    [SerializeField] private Enemy enemy;

    public bool isPortal;

    private void Start()
    {
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (isPortal == true)
            return;

        if (enemy.isDead)
        {
            for (int i = 0; i < portals.Length; i++)
            {
                portals[i].SetActive(true);
            }

            isPortal = true;
        }
    }

}
