using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItemBug : NoPickupItem
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        var player = PlayerManager.instance.player;
        player.SetZeroVelocity();

    }
}
