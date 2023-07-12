using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{

    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();


    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        //ブラックホールスキル
        if (Input.GetKeyDown(KeyCode.R) && player.skill.blackhole.blackholeUnlocked && player.isAction == true)
        {
            if (player.skill.blackhole.cooldownTimer > 0)
            {
                player.fx.CreatePopUpText("スキル使用不可");
                return;
            }
            stateMachine.ChangeState(player.blackHole);
            AudioManager.Instance.PlaySE(AudioManager.SE.blackhole, null);
        }

        //ソードスキル
        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword() && player.skill.sword.swordUnlocked && player.isAction == true)
        {
            stateMachine.ChangeState(player.aimSword);
            //CreateSwordでサウンドならしている
        }

        //パリィスキル
        if (Input.GetKeyDown(KeyCode.Q) && player.skill.parry.parryUnlocked && player.isAction == true)
        {
            stateMachine.ChangeState(player.counterAttack);
            AudioManager.Instance.PlaySE(AudioManager.SE.parry, null);
        }

        //通常攻撃
        if (Input.GetKeyDown(KeyCode.Mouse0) && player.isAction == true)
        {
            stateMachine.ChangeState(player.primaryAttack);
            //PlayerAttackStateでサウンドならしている
        }

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected() && player.isAction == true)
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }

        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
