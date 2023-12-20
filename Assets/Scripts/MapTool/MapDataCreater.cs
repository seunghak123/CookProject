using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Seunghak.Common;
using Newtonsoft.Json;
#if UNITY_EDITOR
using UnityEditor;
#endif

public struct IngameMapObject
{
    public int objectId;
    public Transform objectTransform;
}
public struct IngameMapObjectInfos
{
    public List<IngameMapObject> objectLists;
}
public class MapDataCreater : MonoBehaviour
{
    [SerializeField] private Transform createTransform;
    public void CreateMapObjects(int mapId)
    {
        if (createTransform.childCount != 0)
        {
            for (int i = createTransform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(createTransform.GetChild(i).gameObject);
            }
        }
        IngameMapObjectInfos newMapInfos = JsonConvert.DeserializeObject<IngameMapObjectInfos>(GameResourceManager.Instance.LoadObject("filename").ToString());

        if (newMapInfos.objectLists!=null && newMapInfos.objectLists.Count > 0)
        {
            for(int i=0;i<newMapInfos.objectLists.Count;i++)
            {
                //만드신 Creater이용해서 생성
            }
        }
        //파일을 읽고 생성하는 작업
    }
    public void ReadMapFile()
    {
        //
        if (createTransform.childCount != 0)
        {
            for (int i = createTransform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(createTransform.GetChild(i).gameObject);
            }
        }
    }

#if UNITY_EDITOR
    [SerializeField] private string createFileName;
    [SerializeField] private string createFilePath = $"{FileUtils.GetStreamingAssetsPath()}{FileUtils.GetPlatformString()}";
    public void CreateMapDataFile()
    {
        if (createTransform.childCount == 0)
        {
            return;
        }
        IngameMapObjectInfos mapInfos;
        mapInfos.objectLists = new List<IngameMapObject>();
        for (int i = createTransform.childCount - 1; i >= 0; i--)
        {
            //읽고 저장
            GameObject subObject = createTransform.GetChild(i).gameObject;

            BaseObject baseSubObject = subObject.GetComponent<BaseObject>();
            if(baseSubObject==null)
            {
                continue;
            }
            IngameMapObject objectInfo;
            objectInfo.objectId = baseSubObject.OBJECT_ID;
            objectInfo.objectTransform = subObject.transform;
        }
        if(mapInfos.objectLists.Count>0)
        {
            FileUtils.SaveFile<IngameMapObjectInfos>(createFilePath, createFileName, mapInfos);
        }
    }
#endif
}
[CustomEditor(typeof(MapDataCreater))]
public class MapToolCreateButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapDataCreater mapTool = (MapDataCreater)target;

        if(GUILayout.Button("Create MapFile"))
        {
            //mapTool.GenerateCubes();
        }
    }
}
