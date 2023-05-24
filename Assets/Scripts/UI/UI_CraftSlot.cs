using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    protected override void Start()
    {
        base.Start();

        //初期のクラフト装備表示が剣ではなく鎧になる、一時的に修正（craftEquipmentをpublicに変更）
        ui.craftWindow.SetupCraftWindow
            (GameObject.Find("SetupCraftList-Weapon").GetComponent<UI_CraftList>().craftEquipment[0]);
    }

    public void SetUpCraftSlot(ItemData_Equipment _data)
    {

        if(_data == null)
        {
            return;
        }

        item.data = _data;

        itemImage.sprite = _data.icon;
        itemText.text = _data.itemName;

        /*
        //他の装備品を考慮して決定
        if(itemText.text.Length > 5)
        {
            itemText.fontSize = itemText.fontSize * 0.8f;
        }
        else
        {
            itemText.fontSize = 24;
        }
        */
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);
    }
}
