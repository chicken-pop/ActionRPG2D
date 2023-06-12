using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Bug : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bugCountText;


    private void Update()
    {
        bugCountText.text = $"Žc‚èƒoƒO:{BugEventManager.instance.BugCount}/{BugEventManager.instance.AllBugCount}";
    }
}
