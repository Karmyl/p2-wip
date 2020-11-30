using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int blockID;

    [SerializeField] private Canvas canvas;
    public Vector3 defaultPosition;
    public bool droppedOnSlot;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    AudioManager audiomanager;

    private void Awake()
    {
        audiomanager = FindObjectOfType<AudioManager>();

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        defaultPosition = GetComponent<RectTransform>().localPosition;
        defaultPosition = this.rectTransform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        droppedOnSlot = false;
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (droppedOnSlot)
        {
            this.rectTransform.localPosition = defaultPosition;
            audiomanager.PlaySound("coin collected");
            //audiomanager.PlaySound("dino_osuu_palikkaan");

        }
        else
        {
            this.rectTransform.localPosition = defaultPosition;
            audiomanager.PlaySound("lane missed");

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

}
