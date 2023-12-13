using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MapToolEditor : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private float distance;
    [SerializeField] private int cubeCount;
#if UNITY_EDITOR
    public void GenerateCubes()
    {
        if (transform.childCount != 0)
        { 
            for (int i = transform.childCount - 1; i >= 0; i--)
            { 
                DestroyImmediate(transform.GetChild(i).gameObject); 
            } 
        }
        for (int i = 0; i < cubeCount; i++) 
        { 
            var newCube = Instantiate(cubePrefab);
            newCube.transform.SetParent(gameObject.transform);
            newCube.transform.localPosition = new Vector3(0f, 0f, i * distance); 
            newCube.transform.localRotation = Quaternion.identity;
        }
    }
#endif
}
[CustomEditor(typeof(MapToolEditor))]
public class MapToolCreateButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapToolEditor mapTool = (MapToolEditor)target;

        if(GUILayout.Button("Create MapFile"))
        {
            mapTool.GenerateCubes();
        }
    }
}
