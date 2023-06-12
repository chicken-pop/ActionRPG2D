using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorySceneUI_Likelihood : MonoBehaviour
{
    [SerializeField] private Slider likelihoodSlider;
    [SerializeField] private TextMeshProUGUI likelihoodText;

    private void Start()
    {
        likelihoodSlider.value = 0f;
        likelihoodText.text = $"�D���x:{StorySceneManager.instance.ImpressionPoint}%";
    }

    private void Update()
    {
        SetLikelihood();
    }

    private void SetLikelihood()
    {
        likelihoodSlider.value = (float)StorySceneManager.instance.ImpressionPoint / 100;
        likelihoodText.text = $"�D���x:{StorySceneManager.instance.ImpressionPoint}%";
    }
}
