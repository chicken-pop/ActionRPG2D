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

        //落とす可能性のあるアイテムをリストに格納
        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].DropChance)
            {
                dropList.Add(possibleDrop[i]);
            }
        }

        //リストからアイテムをだす
        for (int i = 0; i < possibleItemDrop; i++)
        {
            //リストがカラの場合を排除する
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

        //アイテムが飛び出す方向
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));

        //アイテムのデータや画像の設定
        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
