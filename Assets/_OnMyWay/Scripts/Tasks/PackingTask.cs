using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackingTask : TaskBehaviour
{
    [SerializeField] List<GameObject> goodItems;
    [SerializeField] List<GameObject> badItems;
    [SerializeField] List<GameObject> normalItems;

    [SerializeField] Destination destination;

    public List<Transform> startingSlots;

    private List<GameObject> selectionOfObjects;

    // Start is called before the first frame update
    void Start()
    {
        destination = FindObjectOfType<Destination>();
        InstantiateSuitcaseItems(startingSlots);
    }

    private void InstantiateSuitcaseItems(List<Transform> slots)
    {
        if(slots == null || slots.Count == 0) return;

        int numberOfBad = 2;
        int numberOfGood = (int) UnityEngine.Random.Range(2, Math.Min(5, goodItems.Count));
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
        for(int i = 0; i < slots.Count; i++) {
            selectionOfObjects[i].transform.position = slots[i].position;
            selectionOfObjects[i].GetComponent<SuitcaseItem>().SetDestinationReference(destination);
        }
    }

    private void InstantiateItems(int numberOfItems, List<GameObject> listOfItems, List<int> usedIndexes, List<GameObject> selectionOfObjects)
    {
        for(int i = 0; i < numberOfItems; i++) {
            int index = GetUnusedItem(listOfItems, usedIndexes);
            selectionOfObjects.Add(GameObject.Instantiate(listOfItems[index]));
            usedIndexes.Add(index);
        }
    }

    private int GetUnusedItem(List<GameObject> listOfItems, List<int> usedIndexes)
    {
        if(usedIndexes.Count == listOfItems.Count) {
            Debug.LogError("Already using all items of type");
            return 0;
        }

        int newIndex = -1;
        do {
            newIndex = (int)UnityEngine.Random.Range(0, listOfItems.Count);
            Debug.Log("newIndex = " + newIndex);
        } while(usedIndexes.Contains(newIndex));
        return newIndex;
    }

    public override void ActivateTask()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
}
