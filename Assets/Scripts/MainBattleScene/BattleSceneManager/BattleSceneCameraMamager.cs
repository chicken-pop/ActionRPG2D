using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleSceneCameraMamager : SingletonMonoBehaviour<BattleSceneCameraMamager>
{
    [SerializeField]
    private List<CinemachineVirtualCamera> cinemachineVirtualCameras = new List<CinemachineVirtualCamera>();

    private enum CameraMode
    {
        Normal,
        Boss
    }

    public override void Awake()
    {
        isSceneinSingleton = true;
    }

    public void ResetCameraSetting()
    {
        cinemachineVirtualCameras[(int)CameraMode.Boss].Priority = 1;
    }

    public void BossEventCameraSetting()
    {
        cinemachineVirtualCameras[(int)CameraMode.Boss].Priority = 11;
    }
}
