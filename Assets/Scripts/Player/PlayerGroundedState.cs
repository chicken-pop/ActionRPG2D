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

        //�u���b�N�z�[���X�L��
        if (Input.GetKeyDown(KeyCode.R) && player.skill.blackhole.blackholeUnlocked)
        {
            if (player.skill.blackhole.cooldownTimer > 0)
            {
                player.fx.CreatePopUpText("�X�L���g�p�s��");
                return;
            }

            stateMachine.ChangeState(player.blackHole);
        }

        //�\�[�h�X�L��
        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword() && player.skill.sword.swordUnlocked)
        {
            stateMachine.ChangeState(player.aimSword);
        }

        //�p���B�X�L��
        if (Input.GetKeyDown(KeyCode.Q) && player.skill.parry.parryUnlocked)
        {
            stateMachine.ChangeState(player.counterAttack);
        }

        //�ʏ�U��
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        //�W�����v
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
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
