using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ClickableElement))]
public class FoodPlate : TaskBehaviour
{
    public List<Sprite> spriteSequence;
    private int _currentState;

    SpriteRenderer _spriteRenderer;
    ClickableElement _clickable;
    ChangeScaleOnHover _scaleFeedback;
    Collider2D _coll;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        this.SetState(0);

        _clickable = GetComponent<ClickableElement>();
        _clickable.onPointerUpCallback += Interact;

        _coll = GetComponent<Collider2D>();
        _scaleFeedback = GetComponent<ChangeScaleOnHover>();
    }

    void SetState(int index) {
        _currentState = index;
        this.SetSprite(_currentState);
    }

    void SetSprite(int index) {
        if(index < spriteSequence.Count && index >= 0) {
            _spriteRenderer.sprite = spriteSequence[index];
        }
    }


    public void Interact() {
        if(_currentState < spriteSequence.Count) {
            SetState(_currentState + 1);
        }

        if(_currentState == spriteSequence.Count - 1) {
            OnComplete();
        }
    }

    public override void ActivateTask()
    {
        _coll.enabled = true;
        _scaleFeedback.enabled = true;
    }
}
