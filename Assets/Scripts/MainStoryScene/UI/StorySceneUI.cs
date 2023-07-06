using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StorySceneUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image img;
    public Sprite usualImage { get; private set; }

    [SerializeField] private Sprite hoverImage;
    public Sprite clickImage;

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
