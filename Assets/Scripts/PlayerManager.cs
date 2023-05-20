using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    public int SkillPoint;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
        
    }

    public bool HaveEnoughSkillPoint(int _point)
    {
        if(_point > SkillPoint)
        {
            return false;
        }

        SkillPoint = SkillPoint - _point;
        return true;
    }
}
