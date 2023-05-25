using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour , ISaveManager
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

    public int GetSkillPoint() => SkillPoint;


    public void LoadData(GameData _data)
    {
        this.SkillPoint = _data.SkillPoint;
    }

    public void SaveData(ref GameData _data)
    {
        _data.SkillPoint = this.SkillPoint;
    }
}
