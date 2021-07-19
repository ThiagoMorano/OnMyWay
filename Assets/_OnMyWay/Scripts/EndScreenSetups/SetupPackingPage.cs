using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;


public class SetupPackingPage : MonoBehaviour
{
    public Transform[] slots;
    public SuitcaseItem[] itemPrefabs;
    public LocalizeStringEvent noteAboutItems;

    public GameObject textBad;

    private SuitcaseItemType[] packedItems;
    private bool badItemFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        packedItems = new SuitcaseItemType[slots.Length];

        for (int i = 0; i < packedItems.Length; i++)
        {
            packedItems[i] = (SuitcaseItemType)PlayerPrefs.GetInt(string.Format("packed{0}", i), UnityEngine.Random.Range(1, 19));
            SuitcaseItem prefab = GetItemPrefab(packedItems[i]);
            InstantiateInSlot(prefab, i);
            if (prefab.cathegory == SuitcaseItem.PackCathegory.BAD)
            {
                badItemFlag = true;
            }
        }


        textBad?.SetActive(badItemFlag);
    }

    private SuitcaseItem GetItemPrefab(SuitcaseItemType suitcaseItemType)
    {
        foreach (var item in itemPrefabs)
        {
            if (item.type == suitcaseItemType)
            {
                return item;
            }
        }
        return null;
    }

    private void InstantiateInSlot(SuitcaseItem prefab, int i)
    {
        if (prefab == null) return;
        SuitcaseItem instantiatedItem = Instantiate(prefab, slots[i]);
        instantiatedItem.transform.localPosition = Vector3.zero;
        instantiatedItem.transform.localEulerAngles = Vector3.zero;
    }
}
