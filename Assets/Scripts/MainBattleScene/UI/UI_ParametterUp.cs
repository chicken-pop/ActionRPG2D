using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ParametterUp : MonoBehaviour, ISaveManager
{
    public Player player { get; private set; }
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private Event eventData;

    public int StrengthLevel;
    public int AgilityLevel;
    public int IntelegenceLevel;
    public int VitalityLevel;

    [Header("Skillpoint info")]
    [SerializeField] private TextMeshProUGUI currentSkillPoint;
    [SerializeField] private float skillPointAmount;

    private void Awake()
    {
        player = PlayerManager.instance.player;
    }

    private void Start()
    {
        UpdateSkillPoint();
    }


    private void Update()
    {
        UpdateSkillPoint();
    }

    public void AddParametter()
    {
        player.stats.strength.AddModifier(StrengthLevel);
        player.stats.agility.AddModifier(AgilityLevel);
        player.stats.intelegence.AddModifier(IntelegenceLevel);
        player.stats.vitality.AddModifier(VitalityLevel);
    }

    private void UpdateSkillPoint()
    {
        skillPointAmount = PlayerManager.instance.GetSkillPoint();
        currentSkillPoint.text = ((int)skillPointAmount).ToString();
    }

    public void CloseWizardEventUI()
    {
        eventData.wizardEvent = true;

        gameObject.SetActive(false);
        inGameUI.SetActive(true);

        BattleSceneGameManager.instance.PauseGame(false);
    }

    public void LoadData(GameData _data)
    {
        this.StrengthLevel = _data.StrengthLevel;
        this.AgilityLevel = _data.AgilityLevel;
        this.IntelegenceLevel = _data.IntelegenceLevel;
        this.VitalityLevel = _data.VitalityLevel;
    }

    public void SaveData(ref GameData _data)
    {
        _data.StrengthLevel = this.StrengthLevel;
        _data.AgilityLevel = this.AgilityLevel;
        _data.IntelegenceLevel = this.IntelegenceLevel;
        _data.VitalityLevel = this.VitalityLevel;
    }
}
