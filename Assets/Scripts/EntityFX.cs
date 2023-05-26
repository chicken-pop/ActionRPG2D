using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("After image fx")]
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorlooseRate;
    [SerializeField] private float afterImageCooldown;
    private float afetrImageCooldownTimer;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;

    [Header("Ailment colors")]
    [SerializeField] private Color[] ignitedColor;
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] shockColor;

    [Header("Ailment particals")]
    [SerializeField] private ParticleSystem igniteFx;
    [SerializeField] private ParticleSystem chilleFx;
    [SerializeField] private ParticleSystem shockFx;

    [Header("Hit FX")]
    [SerializeField] private GameObject hitFx;
    [SerializeField] private GameObject criticalHitFx;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        originalMat = sr.material;
    }

    private void Update()
    {
        afetrImageCooldownTimer -= Time.deltaTime;
    }

    public void CreateAfterImage()
    {
        if (afetrImageCooldownTimer < 0)
        {
            afetrImageCooldownTimer = afterImageCooldown;

            GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(colorlooseRate, sr.sprite);
        }
    }

    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMat;

    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;

        igniteFx.Stop();
        chilleFx.Stop();
        shockFx.Stop();
    }

    public void IgniteFxFor(float _seconds)
    {
        igniteFx.Play();
        InvokeRepeating("IgniteColorFx", 0, 0.15f);
        Invoke("CancelColorChange", _seconds);
    }

    public void ChillFxFor(float _seconds)
    {
        chilleFx.Play();
        InvokeRepeating("ChillColorFx", 0, 0.15f);
        Invoke("CancelColorChange", _seconds);
    }

    public void ShockFxFor(float _seconds)
    {
        shockFx.Play();
        InvokeRepeating("ShockColorFx", 0, 0.15f);
        Invoke("CancelColorChange", _seconds);
    }

    private void IgniteColorFx()
    {
        if (sr.color != ignitedColor[0])
        {
            sr.color = ignitedColor[0];
        }
        else
        {
            sr.color = ignitedColor[1];
        }
    }

    private void ChillColorFx()
    {
        if (sr.color != chillColor[0])
        {
            sr.color = chillColor[0];
        }
        else
        {
            sr.color = chillColor[1];
        }
    }

    private void ShockColorFx()
    {
        if (sr.color != shockColor[0])
        {
            sr.color = shockColor[0];
        }
        else
        {
            sr.color = shockColor[1];
        }
    }

    public void CreateHitFx(Transform _target, bool _critical)
    {
        float zRotation = Random.Range(-90, 90);
        float xPosition = Random.Range(-0.5f, 0.5f);
        float yPosition = Random.Range(-0.5f, 0.5f);

        Vector3 hitFxRotation = new Vector3(0, 0, zRotation);

        GameObject hitPrefab = hitFx;

        //クリティカルの場合
        if (_critical)
        {
            hitPrefab = criticalHitFx;

            float yRotation = 0;
            zRotation = Random.Range(0, 45);

            if (GetComponent<Entity>().facingDir == -1)
            {
                yRotation = 180;
            }

            hitFxRotation = new Vector3(0, yRotation, zRotation);
        }

        GameObject newHitFx = Instantiate(hitPrefab, _target.position + new Vector3(xPosition, yPosition), Quaternion.identity);

        newHitFx.transform.Rotate(hitFxRotation);

        Destroy(newHitFx, 0.6f);
    }
}
