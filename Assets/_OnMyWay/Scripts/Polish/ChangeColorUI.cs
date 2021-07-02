using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeColorUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public Color defaultColor = new Color(1, 1, 1, 1);
    public Color colorOnHover = new Color(1, 1, 1, 1);
    public Color colorOnClick = new Color(0.65f, 0.65f, 0.65f, 1);

    private Image image;

    private bool _isHovering;
    private bool _isClicking;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update() {
        if(!_isHovering && !_isClicking) {
            image.color = defaultColor;
        } else {
            if (_isClicking) {
                image.color = colorOnClick;
            } else if(_isHovering) {
                image.color = colorOnHover;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClicking = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isClicking = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
        // _isClicking = false;
    }
}
