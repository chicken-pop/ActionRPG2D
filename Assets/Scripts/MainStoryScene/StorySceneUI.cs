using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StorySceneUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image img;
    private Sprite usualImage;

    [SerializeField] private Sprite hoverImage;
    [SerializeField] private Sprite clickImage;

    private void Start()
    {
        img = GetComponent<Image>();
        usualImage = img.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = hoverImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = usualImage;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        img.sprite = clickImage;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = usualImage;
    }
}
