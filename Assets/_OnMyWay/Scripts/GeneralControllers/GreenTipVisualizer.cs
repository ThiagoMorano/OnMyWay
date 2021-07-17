using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTipVisualizer : MonoBehaviour
{
    [System.Serializable]
    public struct GreenTip
    {
        public GameObject collpased;
        public GameObject expanded;
    }

    [SerializeField] public GreenTip[] tips;


    void Start()
    {
        CollapseAll();
    }

    public void CollapseTip(int index)
    {
        if (index < 0 || index >= tips.Length) return;

        tips[index].collpased.SetActive(true);
        tips[index].expanded.SetActive(false);
    }

    public void ExpandTip(int index)
    {
        if (index < 0 || index >= tips.Length) return;

        CollapseAll();

        tips[index].collpased.SetActive(false);
        tips[index].expanded.SetActive(true);
    }

    public void CollapseAll()
    {
        for (int i = 0; i < tips.Length; i++)
        {
            CollapseTip(i);
        }
    }
}
