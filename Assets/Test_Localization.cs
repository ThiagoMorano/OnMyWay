using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Components;
using TMPro;


public class Test_Localization : MonoBehaviour
{
    public LocalizeStringEvent localizeStringEvent;

    public LocalizedStringTable _localizedStringTable;
    public StringTable _currentStringTable;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        var tableLoading = _localizedStringTable.GetTable();
        yield return tableLoading;

        _currentStringTable = tableLoading;

        string str = _currentStringTable["languages"].LocalizedValue;

        localizeStringEvent = GetComponent<LocalizeStringEvent>();
        // localizeStringEvent.StringReference.
        string entryName = localizeStringEvent.StringReference.TableEntryReference.Key;
        Debug.Log(localizeStringEvent.StringReference.TableEntryReference);
        Debug.Log(entryName);
        // Debug.Log(localizeStringEvent.StringReference);

        GetComponent<TextMeshProUGUI>().text = str;
    }
}
