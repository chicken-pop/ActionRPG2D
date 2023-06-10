using UnityEngine;

[CreateAssetMenu(fileName = "New FlagData", menuName = "Data/FlagData")]
public sealed class FlagData : ScriptableObject
{
    public bool IsOn = false;

    public void ChangeFlagStatus()
    {
        IsOn = true;
    }
}