using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UI ui;
    private Image skillImage;

    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;
    [SerializeField] private Color lockedSkillColor;

    public bool unlocked;

    [SerializeField] private UI_SkillTreeSlot[] sholdBeUnlocked; //解放されているスキル
    [SerializeField] private UI_SkillTreeSlot[] sholdBeLocked;

    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI-" + skillName;
    }

    private void Start()
    {
        skillImage = GetComponent<Image>();
        ui = GetComponentInParent<UI>();

        skillImage.color = lockedSkillColor;

        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
    }

    public void UnlockSkillSlot()
    {
        //スキルを取得するための元のスキルが解放されていなかったらreturn
        for (int i = 0; i < sholdBeUnlocked.Length; i++)
        {
            if (sholdBeUnlocked[i].unlocked == false)
            {
                Debug.Log("Cannot unlock skill");
                return;
            }
        }

        //一定のスキルが既に解放されていたらreturn
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
        float yOffset = 65;

        Vector2 mousePosition = Input.mousePosition;

        Debug.Log(mousePosition);

        if (mousePosition.x > 400)
        {
            xOffset = -175;
        }
        else
        {
            xOffset = 175;
        }

        //マウスのポジションによって表示場所を変える
        ui.skillToolTip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }


}
