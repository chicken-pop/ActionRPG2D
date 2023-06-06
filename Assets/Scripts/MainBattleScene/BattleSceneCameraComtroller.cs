using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneCameraComtroller : MonoBehaviour
{
    //‚¤‚Ü‚­‚¢‚©‚È‚©‚Á‚½‚Ì‚ÅŒã‚Ål‚¦‚é


    private CinemachineVirtualCamera chinemachineCamera => GetComponent<CinemachineVirtualCamera>();

    private Vector3 cameraPos;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainBattleSceneSnowyMountain")
        {
            if (PlayerManager.instance.player.transform.position.x < -8.5f)
            {
                cameraPos = chinemachineCamera.transform.position;
                chinemachineCamera.enabled = false;
            }
            else
            {
                transform.position = cameraPos;
                chinemachineCamera.enabled = true;
            }
        }

    }
}
