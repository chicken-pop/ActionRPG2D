using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , ISaveManager
{
    private UI ui;
    private Image skillImage;

    [SerializeField] private int consumptionPoint;
    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;
    [SerializeField] private Color lockedSkillColor;

    public bool unlocked;

    [SerializeField] private UI_SkillTreeSlot[] sholdBeUnlocked; //�������Ă���X�L��
    [SerializeField] private UI_SkillTreeSlot[] sholdBeLocked;

    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI-" + skillName;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
    }

    private void Start()
    {
        skillImage = GetComponent<Image>();
        ui = GetComponentInParent<UI>();

        skillImage.color = lockedSkillColor;

        if (unlocked)
        {
            skillImage.color = Color.white;
        }
    }

    public void UnlockSkillSlot()
    {
        //�X�L����������邽�߂̏\���ȃX�L���|�C���g�������Ă��邩�ǂ���
        if(PlayerManager.instance.HaveEnoughSkillPoint(consumptionPoint) == false)
        {
            return;
        }

        //�X�L�����擾���邽�߂̌��̃X�L�����������Ă��Ȃ�������return
        for (int i = 0; i < sholdBeUnlocked.Length; i++)
        {
            if (sholdBeUnlocked[i].unlocked == false)
            {
                Debug.Log("Cannot unlock skill");
                return;
            }
        }

        //���̃X�L�������ɉ������Ă�����return
        for (int i = 0; i < sholdBeLocked.Length; i++)
        {
            if (sholdBeLocked[i].unlocked == true)
            {
                Debug.Log("Cannot unlock skill");
                return;
            }
        }


        unlocked = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(skillDescription, skillName);

        float xOffset = 0;
        float yOffset = 0;

        Vector2 pos = gameObject.transform.position;

        if (pos.x > 960)
        {
            xOffset = -400;
        }
        else
        {
            xOffset = 400;
        }

        if (pos.y > 700)
        {
            yOffset = -160;
        }
        else
        {
            yOffset = 160;
        }

        //�|�W�V�����ɂ���ĕ\���ꏊ��ς���
        ui.skillToolTip.transform.position = new Vector2(pos.x + xOffset, pos.y + yOffset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }

    public void LoadData(GameData _data)
    {
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlocked = value;
        }
    }

    public void SaveData(ref GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName, out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlocked);
        }
        else
        {
            _data.skillTree.Add(skillName, unlocked);
        }
    }
}