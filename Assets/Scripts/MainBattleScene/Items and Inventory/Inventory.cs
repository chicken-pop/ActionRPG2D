using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour , ISaveManager
{
    public static Inventory instance;

    public List<ItemData> startingItems;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmnetSlotParent;
    [SerializeField] private Transform[] statSlotParent; //Character_UI��WizardEvent_UI�̃p�����[�^�\���̂���

    private UI_ItemSlot[] inventoryItemSlot;
    private UI_ItemSlot[] stashItemSlot;
    private UI_EquipmentSlot[] equipmentSlot;
    private UI_StatSlot[] statSlot;
    private UI_StatSlot[] statSlotWizardEvent;

    [Header("Item cooldown")]
    private float lastTimeUseFlask;
    private float lastTimeUseArmor;
    public float flaskCooldown { get; private set; }
    private float armorCooldown;

    [Header("Data base")]
    public List<InventoryItem> loadedItems;
    public List<ItemData_Equipment> loadedEquipment;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmnetSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();

        statSlot = statSlotParent[0].GetComponentsInChildren<UI_StatSlot>();
        statSlotWizardEvent = statSlotParent[1].GetComponentsInChildren<UI_StatSlot>();

        Invoke("AddStartingItem", 0.1f);
        //AddStartingItem();
    }

    private void AddStartingItem()
    {
        foreach (ItemData_Equipment item in loadedEquipment)
        {
            EquipItem(item);
        }

        if(loadedItems.Count > 0)
        {
            foreach (InventoryItem item in loadedItems)
            {
                for (int i = 0; i < item.stackSize; i++)
                {
                    AddItem(item.data);
                }
            }

            return;
        }


        for (int i = 0; i < startingItems.Count; i++)
        {
            if (startingItems[i] != null)
            {
                AddItem(startingItems[i]);
            }
        }
    }

    private void UpdateSlotUI()
    {
        //������SlotUI���X�V
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].UpdateSlot(item.Value);
                }
            }
        }

        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stash[i]);
        }

        //�L�����N�^�[�̃X�e�[�^�XUI�̍X�V
        UpdateStatsUI();
    }

    public void UpdateStatsUI()
    {
        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateStatValueUI();
        }

        for (int i = 0; i < statSlotWizardEvent.Length; i++)
        {
            statSlotWizardEvent[i].UpdateStatValueUI();
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemData_Equipment oldeEquipment = null;

        //Dictionary���ő������Ă��邩�𔻒肷��
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                oldeEquipment = item.Key;
            }
        }

        //�������Ă����ꍇ�A�����i���Ƃ�
        if (oldeEquipment != null)
        {
            UnequipItem(oldeEquipment);
            AudioManager.Instance.PlaySE(AudioManager.SE.equipmentOff, null);
            AddItem(oldeEquipment);

        }

        //��������(�����i�ɂ�鋭��)�A�C���x���g�����������
        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();
        RemoveItem(_item);
        AudioManager.Instance.PlaySE(AudioManager.SE.equipmentOn, null);

        UpdateSlotUI();
    }

    public void UnequipItem(ItemData_Equipment itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }

    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment && CanAddItem())
        {
            AddToInventory(_item);
        }
        else if (_item.itemType == ItemType.Material)
        {
            AddToStash(_item);
        }

        UpdateSlotUI();
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            //���ɃA�C�e���������Ă�����A�������iStack�j�𑝂₷
            value.AddStack();
        }
        else
        {
            //�C���x���g���ɕ����ǉ�����
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }

    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            //���ɃA�C�e���������Ă�����A�������iStack�j�𑝂₷
            value.AddStack();
        }
        else
        {
            //�C���x���g���ɃA�C�e����ǉ�����
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }

        if (stashDictionary.TryGetValue(_item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }

        UpdateSlotUI();
    }

    public bool CanAddItem()
    {
        if (inventory.Count >= inventoryItemSlot.Length)
        {
            return false;
        }

        return true;
    }

    public bool CanCraft(ItemData_Equipment _itemToCraft, List<InventoryItem> _requireMaterials)
    {
        List<InventoryItem> materialsToRemove = new List<InventoryItem>();

        for (int i = 0; i < _requireMaterials.Count; i++)
        {
            //�A�C�e�������邩�̊m�F���s��
            if (stashDictionary.TryGetValue(_requireMaterials[i].data, out InventoryItem stashValue))
            {
                //�A�C�e���̗ʂ�����Ă��邩
                if (stashValue.stackSize < _requireMaterials[i].stackSize)
                {
                    return false;
                }
                else
                {
                    materialsToRemove.Add(stashValue);
                }
            }
            else
            {
                return false;
            }
        }

        //�A�C�e���̏���
        for (int i = 0; i < materialsToRemove.Count; i++)
        {
            RemoveItem(materialsToRemove[i].data);
        }

        //�N���t�g����
        AddItem(_itemToCraft);
        AudioManager.Instance.PlaySE(AudioManager.SE.craft, null);

        return true;
    }

    public List<InventoryItem> GetEquipmentList() => equipment;

    public List<InventoryItem> GetStashList() => stash;

    public ItemData_Equipment GetEquipment(EquipmentType _type)
    {
        ItemData_Equipment equipedItem = null;

        //�����i��Dictionary���Ń^�C�v�̔��ʂ��s��
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == _type)
            {
                equipedItem = item.Key;
            }
        }

        return equipedItem;
    }

    public void UseFlask()
    {
        //�^�C�v�̔��ʂ��s��
        ItemData_Equipment currentFlask = GetEquipment(EquipmentType.Flask);

        if (currentFlask == null)
        {
            return;
        }

        //�A�C�e�����g���邩���f
        bool canUseFlask = Time.time > lastTimeUseFlask + flaskCooldown;

        if (canUseFlask)
        {
            flaskCooldown = currentFlask.itemCoolDown;
            //�A�C�e���̌��ʂ̎��s
            currentFlask.Effect(null);
            lastTimeUseFlask = Time.time;
        }
        else
        {
            Debug.Log("Flask on cooldown");
        }
    }

    public bool CanUseArmor()
    {
        //�^�C�v�̔��ʂ��s��
        ItemData_Equipment currentArmor = GetEquipment(EquipmentType.Armor);

        //�A�C�e�����g���邩���f
        if (Time.time > lastTimeUseArmor + armorCooldown)
        {
            armorCooldown = currentArmor.itemCoolDown;
            lastTimeUseArmor = Time.time;
            return true;
        }

        Debug.Log("Armor on cooldown");
        return false;
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, int> pair in _data.inventory)
        {
            foreach (var item in GetItemDataBase())
            {
                if(item != null && item.itemID == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = pair.Value;

                    loadedItems.Add(itemToLoad);
                }
            }
        }

        foreach (string loadedItemID in _data.equipmentID)
        {
            foreach (var item in GetItemDataBase())
            {
                if (item != null && loadedItemID == item.itemID)
                {
                    loadedEquipment.Add(item as ItemData_Equipment);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equipmentID.Clear();

        foreach (KeyValuePair<ItemData, InventoryItem> pair in inventoryDictionary)
        {
            _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
        }

        foreach (KeyValuePair<ItemData, InventoryItem> pair in stashDictionary)
        {
            _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
        }

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> pair in equipmentDictionary)
        {
            _data.equipmentID.Add(pair.Key.itemID);
        }

    }

    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Items" });

        foreach (string SOName  in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }

        return itemDataBase;
    }
}
