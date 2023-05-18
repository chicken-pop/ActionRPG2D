using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public void SwitchTo(GameObject _menu)
    {
        //Canvas内を全て非表示にして
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        //表示する
        if (_menu != null)
        {
            _menu.SetActive(true);
        }
    }
}
