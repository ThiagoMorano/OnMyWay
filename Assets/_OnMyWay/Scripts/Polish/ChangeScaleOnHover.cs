using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChangeScaleOnHover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public float scaleModifier = 1.1f;
    public List<Transform> associatedTransforms;
    Vector3 defaultScale;

    private List<Vector3> associatedTransformsDefaultScales;


    // Start is called before the first frame update
    void Start()
    {
        defaultScale = transform.localScale;

        associatedTransformsDefaultScales = new List<Vector3>();
        foreach(var transf in associatedTransforms) {
            associatedTransformsDefaultScales.Add(new Vector3(transf.localScale.x, transf.localScale.y, transf.localScale.z));
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        // defaultScale = transform.localScale;
        transform.localScale = defaultScale * scaleModifier;

        if(associatedTransforms.Count > 0) {
            SetAssociatedTransforms(scaleModifier);
        }
    }

    private void SetAssociatedTransforms(float scaleModifier) {
        for(int i = 0; i < associatedTransforms.Count; i++) {
            associatedTransformsDefaultScales[i] = associatedTransforms[i].localScale;
            associatedTransforms[i].localScale = associatedTransforms[i].localScale * scaleModifier;
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        ResetToDefaultScale();
    }

    private void ResetToDefaultScale()
    {
       transform.localScale = defaultScale;

        if(associatedTransforms.Count > 0) {
            ResetAssociatedTransforms();
        }
    }

    private void ResetAssociatedTransforms()
    {
        for(int i = 0; i < associatedTransforms.Count; i++) {
            associatedTransforms[i].localScale = associatedTransformsDefaultScales[i];
        }
    }
}
