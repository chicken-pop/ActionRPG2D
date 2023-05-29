using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ice and Fire effect", menuName = "Data/Item efect/Ice and Fire")]
public class IceAndFire_Effect : ItemEffect
{
    [SerializeField] private GameObject iceAndFireEffect;
    [SerializeField] private float xVelocity;

    public override void ExecuteEffect(Transform _respaweenPosition)
    {
        Player player = PlayerManager.instance.player;

        bool thirdAttack = player.primaryAttack.comboCounter == 2;

        if (thirdAttack)
        {
            //Prefabwを生成し、プレイヤーが向いている方向に発射する
            GameObject newIceAndFire = Instantiate(iceAndFireEffect, _respaweenPosition.position, player.transform.rotation);
            newIceAndFire.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.facingDir, 0);

            Destroy(newIceAndFire, 10);
        }
    }
}
