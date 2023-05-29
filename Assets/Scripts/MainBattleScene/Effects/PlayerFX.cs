using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFX : EntityFX
{
    [Header("After image fx")]
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorlooseRate;
    [SerializeField] private float afterImageCooldown;
    private float afetrImageCooldownTimer;

    [Header("Screen shake fx")]
    private CinemachineImpulseSource screenShake;
    [SerializeField] private float shakeMultiplier;
    public Vector3 shakeSwordImpact;
    public Vector3 shackHighDamageImpact;

    protected override void Start()
    {
        base.Start();
        screenShake = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        afetrImageCooldownTimer -= Time.deltaTime;
    }

    //ダッシュ時の残像
    public void CreateAfterImage()
    {
        if (afetrImageCooldownTimer < 0)
        {
            afetrImageCooldownTimer = afterImageCooldown;

            GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(colorlooseRate, sr.sprite);
        }
    }

    //画面ゆれ
    public void ScreenShake(Vector3 _shakePower)
    {
        screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * player.facingDir, _shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse();
    }
}
