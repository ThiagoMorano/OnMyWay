using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;


public static class IListExtensions {
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts) {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}

public class PackingTask : TaskBehaviour
{
    [SerializeField] List<GameObject> goodItems;
    [SerializeField] List<GameObject> badItems;
    [SerializeField] List<GameObject> normalItems;

    Destination _destination;

    public List<Transform> startingSlots;

    private List<GameObject> selectionOfObjects;

    public ClickableElement confirmButton;
    public ClickableElement resetButton;
    public LocalizeStringEvent feedbackText;
    public LocalizeStringEvent badItemFeedback;

    // Start is called before the first frame update
    void Start()
    {
        _destination = FindObjectOfType<Destination>();
        InstantiateSuitcaseItems(startingSlots);

        _destination.onAllItemsPlacedCallback += RefreshFeedbackTexts;

        confirmButton.onPointerUpCallback += OnComplete;
        resetButton.onPointerUpCallback += ResetAll;
    }

    private void ResetAll()
    {
        _destination.ResetAll();
    }

    private void InstantiateSuitcaseItems(List<Transform> slots)
    {
        if (slots == null || slots.Count == 0) return;

        int numberOfBad = 2;
        int numberOfGood = (int)UnityEngine.Random.Range(2, Math.Min(5, goodItems.Count));
        int numberOfNormals = Math.Max(0, slots.Count - (numberOfBad + numberOfGood));

        List<int> usedBads = new List<int>();
        List<int> usedGoods = new List<int>();
        List<int> usedNormals = new List<int>();
        selectionOfObjects = new List<GameObject>();

        // Debug.Log("Instantiate bads");
        InstantiateItems(numberOfBad, badItems, usedBads, selectionOfObjects);
        // Debug.Log("Instantiate goods");
        InstantiateItems(numberOfGood, goodItems, usedGoods, selectionOfObjects);
        // Debug.Log("Instantiate normals");
        InstantiateItems(numberOfNormals, normalItems, usedNormals, selectionOfObjects);

        // shuffle selectionOfObjects
        selectionOfObjects.Shuffle();

        for (int i = 0; i < slots.Count; i++)
        {
            // selectionOfObjects[i].transform.position = slots[i].position;
            selectionOfObjects[i].transform.SetParent(slots[i]);
            selectionOfObjects[i].transform.localPosition = Vector3.zero;
            selectionOfObjects[i].GetComponent<SuitcaseItem>().SetDestinationReference(_destination);
        }
    }


    private void InstantiateItems(int numberOfItems, List<GameObject> listOfItems, List<int> usedIndexes, List<GameObject> selectionOfObjects)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            int index = GetUnusedItem(listOfItems, usedIndexes);
            selectionOfObjects.Add(GameObject.Instantiate(listOfItems[index]));
            usedIndexes.Add(index);
        }
    }

    private int GetUnusedItem(List<GameObject> listOfItems, List<int> usedIndexes)
    {
        if (usedIndexes.Count == listOfItems.Count)
        {
            Debug.LogError("Already using all items of type");
            return 0;
        }

        int newIndex = -1;
        do
        {
            newIndex = (int)UnityEngine.Random.Range(0, listOfItems.Count);
        } while (usedIndexes.Contains(newIndex));
        return newIndex;
    }



    private void RefreshFeedbackTexts()
    {
        badItemFeedback.gameObject.SetActive(HasBadItemPacked(_destination.itemsStored));
    }

    private bool HasBadItemPacked(SuitcaseItem[] itemsStored)
    {
        for (int i = 0; i < itemsStored.Length; i++)
        {
            if (itemsStored[i].cathegory == SuitcaseItem.PackCathegory.BAD)
            {
                return true;
            }
        }
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        if (confirmButton != null)
        {
            bool activeValue = _destination.IsComplete;
            feedbackText.gameObject.SetActive(activeValue);
            confirmButton.gameObject.SetActive(activeValue);
            resetButton.gameObject.SetActive(activeValue);
        }
    }



    public override void ActivateTask()
    {

    }
}
