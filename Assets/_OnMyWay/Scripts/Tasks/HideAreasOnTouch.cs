using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HideAreasOnTouch : TaskBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    public int radius = 50;

    [Tooltip("The task is considered completed once this percentage of wiped steam has been achieved")]
    public float percentageForCompletion = 0.6f;
    int numberOfTouchedPixels;

    SpriteRenderer spriteRenderer;
    Material material;
    Sprite runtimeSprite;
    Texture2D texture;
    Color transparent = new Color(0, 0, 0, 0);

    bool _completed;
    bool _isHovering;

    Boundary boundaries;
    struct Boundary
    {
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
        public Vector2 topLeft;
        public Vector2 topRight;
    }

    Collider2D coll;

    // public UnityEvent completeResponse;
    // public Action onCompleteCallback;

    public UnityEvent onDragResponse;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;

        Texture2D originalTexture = spriteRenderer.sprite.texture;

        texture = new Texture2D(originalTexture.width, originalTexture.height);
        texture.SetPixels(originalTexture.GetPixels());

        texture.Apply();
        runtimeSprite = Sprite.Create(texture, new Rect(0, 0, originalTexture.width, originalTexture.height), new Vector2(0.5f, 0.5f), 108f);
        spriteRenderer.sprite = runtimeSprite;

        material.mainTexture = texture;

        numberOfTouchedPixels = 0;


        RecalculateScreenSpaceBounds(spriteRenderer.bounds);
    }

    private void RecalculateScreenSpaceBounds(Bounds bounds)
    {
        Vector3 bottomLeftCorner = new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y);
        Vector3 bottomRightCorner = new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y);
        Vector3 topLeftCorner = new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y);
        Vector3 topRightCorner = new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y);

        boundaries.bottomLeft = Camera.main.WorldToScreenPoint(bottomLeftCorner);
        boundaries.bottomRight = Camera.main.WorldToScreenPoint(bottomRightCorner);
        boundaries.topLeft = Camera.main.WorldToScreenPoint(topLeftCorner);
        boundaries.topRight = Camera.main.WorldToScreenPoint(topRightCorner);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        RecalculateScreenSpaceBounds(spriteRenderer.bounds);
        RemoveSteam(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDragResponse?.Invoke();
        RemoveSteam(eventData.position);
        CheckCompletion();
    }

    private void RemoveSteam(Vector2 pointerPosition)
    {
        if (IsOutOfBounds(pointerPosition))
        {
            // print("Out of bounds");
            return;
        }


        Vector2Int pointerPositionInGrid = new Vector2Int(
            Mathf.Clamp(Mathf.RoundToInt((pointerPosition.x - boundaries.bottomLeft.x) / (boundaries.bottomRight.x - boundaries.bottomLeft.x) * texture.width), 0, texture.width),
            Mathf.Clamp(Mathf.RoundToInt((pointerPosition.y - boundaries.bottomLeft.y) / (boundaries.topLeft.y - boundaries.bottomLeft.y) * texture.height), 0, texture.height)
        );

        for (int i = -radius; i < radius; i++)
        {
            if (pointerPositionInGrid.x + i < 0 || texture.width < pointerPositionInGrid.x + i) continue;
            for (int j = -radius; j < radius; j++)
            {
                if (pointerPositionInGrid.y + j < 0 || texture.height < pointerPositionInGrid.y + j) continue;
                if (i * i + j * j <= radius * radius)
                {
                    if (texture.GetPixel(pointerPositionInGrid.x + i, pointerPositionInGrid.y + j).a != 0)
                    {
                        numberOfTouchedPixels++;
                        texture.SetPixel(pointerPositionInGrid.x + i, pointerPositionInGrid.y + j, transparent);
                    }
                }
            }
        }
        texture.Apply();
        material.mainTexture = texture;
    }

    bool IsOutOfBounds(Vector2 pointerOnScreen)
    {
        return !_isHovering;
    }

    private void CheckCompletion()
    {
        if (!_completed)
        {
            if (((float)numberOfTouchedPixels) / (texture.width * texture.height) > percentageForCompletion)
            {
                _completed = true;
                OnComplete();
            }
        }
    }

    // private void OnComplete()
    // {
    //     Debug.Log("Task completed");

    //     if(onCompleteCallback != null) onCompleteCallback();
    //     completeResponse?.Invoke();
    // }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public override void ActivateTask()
    {
        coll.enabled = true;
    }
}
