using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;


[CustomEditor(typeof(MeshRenderer))]
public class MeshRendererSortingEditor : Editor
{
    private Editor defaultEditor;
    private MeshRenderer meshRenderer;
    private bool showSorting;
    private string header = "2D Sorting";

    private void OnEnable()
    {
        defaultEditor = CreateEditor(targets, Type.GetType("UnityEditor.MeshRendererEditor, UnityEditor"));
        meshRenderer = target as MeshRenderer;
    }

    private void OnDisable()
    {
        //When OnDisable is called, the default editor we created should be destroyed to avoid memory leakage.
        //Also, make sure to call any required methods like OnDisable
        var disableMethod = defaultEditor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (disableMethod != null)
            disableMethod.Invoke(defaultEditor, null);
        DestroyImmediate(defaultEditor);
    }

    public override void OnInspectorGUI()
    {
        defaultEditor.OnInspectorGUI();

        showSorting = EditorGUILayout.BeginFoldoutHeaderGroup(showSorting, header);
        if (showSorting)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            var newId = DrawSortingLayersPopup(meshRenderer.sortingLayerID);
            if (EditorGUI.EndChangeCheck())
            {
                meshRenderer.sortingLayerID = newId;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            var order = EditorGUILayout.IntField("Sorting Order", meshRenderer.sortingOrder);
            if (EditorGUI.EndChangeCheck())
            {
                meshRenderer.sortingOrder = order;
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    static int DrawSortingLayersPopup(int layerID)
    {
        var layers = SortingLayer.layers;
        var names = layers.Select(l => l.name).ToArray();
        if (!SortingLayer.IsValid(layerID))
        {
            layerID = layers[0].id;
        }
        var layerValue = SortingLayer.GetLayerValueFromID(layerID);
        var newLayerValue = EditorGUILayout.Popup("Sorting Layer", layerValue, names);
        return layers[newLayerValue].id;
    }

}
