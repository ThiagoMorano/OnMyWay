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

    SpriteRenderer spriteRenderer;
    ClickableElement clickable;
    ChangeScaleOnHover scaleFeedback;
    Collider2D coll;

    // public UnityEvent completeResponse;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.SetState(0);

        clickable = GetComponent<ClickableElement>();
        clickable.onPointerUpCallback += Interact;

        coll = GetComponent<Collider2D>();
        scaleFeedback = GetComponent<ChangeScaleOnHover>();
    }

    void SetState(int index) {
        _currentState = index;
        this.SetSprite(_currentState);
    }

    void SetSprite(int index) {
        if(index < spriteSequence.Count && index >= 0) {
            spriteRenderer.sprite = spriteSequence[index];
        }
    }


    public void Interact() {
        Debug.Log("interact");

        if(_currentState < spriteSequence.Count) {
            SetState(_currentState + 1);
        }

        if(_currentState == spriteSequence.Count - 1) {
            OnComplete();
        }
    }

    public override void ActivateTask()
    {
        coll.enabled = true;
        scaleFeedback.enabled = true;
    }

    // void OnComplete() {
    //     Debug.Log("Task completed");
    //     completeResponse?.Invoke();
    // }
}
