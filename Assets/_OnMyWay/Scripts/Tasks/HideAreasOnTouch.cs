using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideAreasOnTouch : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    SpriteRenderer spriteRenderer;
    Material material;
    Sprite runtimeSprite;
    Texture2D texture;

    public int numberOfTouchedPixels;

    Color transparent = new Color(0, 0, 0, 0);

    public int radius = 30;

    Collider2D coll;


    struct Boundary {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
    }
    Boundary boundaries;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;

        Texture2D originalTexture = spriteRenderer.sprite.texture;

        texture = new Texture2D(originalTexture.width, originalTexture.height);
        texture.SetPixels(originalTexture.GetPixels());
        // for(int j = 100; j < 120; j++) {
        //     for(int i = 0; i < 588; i++) {
        //         texture.SetPixel(i, j, transparent);
        //     }
        // }
        texture.Apply();
        runtimeSprite = Sprite.Create(texture, new Rect(0, 0, originalTexture.width, originalTexture.height), new Vector2(0.5f, 0.5f), 108f);
        spriteRenderer.sprite = runtimeSprite;

        material.mainTexture = texture;

        numberOfTouchedPixels = 0;


        boundaries = new Boundary();
        boundaries.xMin = transform.position.x - originalTexture.width / 2;
        boundaries.xMax = transform.position.x + originalTexture.width / 2;
        boundaries.yMin = transform.position.y - originalTexture.height / 2;
        boundaries.yMax = transform.position.y + originalTexture.height / 2;

        print(boundaries.xMin);
        print(boundaries.xMax);
        print(boundaries.yMin);
        print(boundaries.yMax);
    }


    public void OnDrag(PointerEventData eventData)
    {
        RemoveSteam(eventData.position);
    }

    private void RemoveSteam(Vector2 pointerPosition)
    {
        Vector2 pointerInWorld = Camera.main.ScreenToWorldPoint(pointerPosition);
        print(pointerInWorld);

        if(IsOutOfBounds(pointerPosition)) {
            print("Out of bounds");
            return;
        }


        Vector2Int pointerPositionInGrid = new Vector2Int(Mathf.RoundToInt(pointerPosition.x / Screen.width * texture.width), 
                                                    Mathf.RoundToInt(pointerPosition.y / Screen.height * texture.height));

        for (int i = -radius ; i < radius; i++) {
            for (int j = -radius; j < radius; j++) {
                if(i*i + j*j <= radius*radius) {
                    if(texture.GetPixel(pointerPositionInGrid.x + i,  pointerPositionInGrid.y + j).a != 0) {
                        numberOfTouchedPixels++;
                        texture.SetPixel(pointerPositionInGrid.x + i, pointerPositionInGrid.y + j, transparent);
                    }
                }
            }
        }
        texture.Apply();
        material.mainTexture = texture;
    }

    bool IsOutOfBounds(Vector2 pointerOnScreen) {
        // if(pointerInWorld.x < boundaries.xMin || pointerInWorld.x > boundaries.xMax ||
        // pointerInWorld.y < boundaries.yMin || pointerInWorld.y > boundaries.yMax) {
        //     print("Out of bounds");
        //     return;
        // }
        return false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        coll.enabled = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        coll.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RemoveSteam(eventData.position);
    }
}
