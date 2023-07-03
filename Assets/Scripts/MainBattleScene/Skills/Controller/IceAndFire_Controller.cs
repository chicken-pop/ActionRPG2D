using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAndFire_Controller : Thunder_Strike_Counroller
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        AudioManager.Instance.PlaySE(AudioManager.SE.iceAndFire, null);
    }
}
