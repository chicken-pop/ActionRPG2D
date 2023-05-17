using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int possibleItemDrop;
    [SerializeField] private ItemData[] possibleDrop;
    private List<ItemData> dropList = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab;

    public virtual void GenerateDrop()
    {
        if (possibleDrop.Length == 0)
        {
            return;
        }

        //���Ƃ��\���̂���A�C�e�������X�g�Ɋi�[
        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].DropChance)
            {
                dropList.Add(possibleDrop[i]);
            }
        }

        //���X�g����A�C�e��������
        for (int i = 0; i < possibleItemDrop; i++)
        {
            //���X�g���J���̏ꍇ��r������
            if (dropList.Count <= 0)
            {
                return;
            }
            
                ItemData randomItem = dropList[Random.Range(0, dropList.Count - 1)];

                dropList.Remove(randomItem);
                DropItem(randomItem);
            

        }
    }

    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        //�A�C�e������яo������
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));

        //�A�C�e���̃f�[�^��摜�̐ݒ�
        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
