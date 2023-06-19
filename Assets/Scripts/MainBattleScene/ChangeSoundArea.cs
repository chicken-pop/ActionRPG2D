using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundArea : MonoBehaviour
{
    [SerializeField] private AudioManager.BGM bgm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            AudioManager.Instance.PlayBGM(bgm);
        }
    }
}
