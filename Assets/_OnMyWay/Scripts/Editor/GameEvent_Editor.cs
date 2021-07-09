using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent)), CanEditMultipleObjects]
public class GameEvent_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUI.enabled = Application.isPlaying;

        GameEvent gameEvent = (GameEvent)target;
        if (GUILayout.Button("Raise"))
        {
            gameEvent.Raise();
        }
    }
}