using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Skill : Skill
{
    [Header("�_�b�V��")]
    [SerializeField] private UI_SkillTreeSlot dashUnlockedButton;
    public bool dashUnlocked { get; private set; }

    [Header("�~���[�W���_�b�V��")]
    [SerializeField] private UI_SkillTreeSlot cloneOnDashUnlockedButton;
    public bool cloneOnDashUnlocked { get; private set; }

    [Header("�~���[�W���_�b�V���{")]
    [SerializeField] private UI_SkillTreeSlot cloneOnArrivalUnlockedButton;
    public bool cloneOnArrivalUnlocked { get; private set; }

    protected override void Start()
    {
        base.Start();

        dashUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        cloneOnDashUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
        cloneOnArrivalUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);
    }

    public override void UseSkill()
    {
        base.UseSkill();
    }

    private void UnlockDash()
    {
        if (dashUnlockedButton.unlocked)
        {
            dashUnlocked = true;
        }
    }

    private void UnlockCloneOnDash()
    {
        if (cloneOnDashUnlockedButton.unlocked)
        {
            cloneOnDashUnlocked = true;
        }
    }

    private void UnlockCloneOnArrival()
    {
        if (cloneOnArrivalUnlockedButton.unlocked)
        {
            cloneOnArrivalUnlocked = true;
        }
    }

    //�~���[�W���_�b�V��
    public void CloneOnDash()
    {
        if (cloneOnDashUnlocked)
        {
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
        }
    }

    //�~���[�W���_�b�V���{
    public void CloneOnArrival()
    {
        if (cloneOnArrivalUnlocked)
        {
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
        }
    }
}
