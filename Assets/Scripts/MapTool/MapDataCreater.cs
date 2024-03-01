using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Seunghak.Common;
using Newtonsoft.Json;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class IngameMapObject
{
    public int toolObjectId;
    public string toolSpriteName;
    public List<float> objectPosition;
    public List<float> objectScale;
    public List<float> objectCollisionSize;
}
[Serializable]
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
    private string createFilePath = $"{FileUtils.GetStreamingAssetsPath()}BaseSources/MapData/MapObjectData";
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

            BaseToolObject baseSubObject = subObject.GetComponent<BaseToolObject>();
            if(baseSubObject==null)
            {
                continue;
            }

            if(baseSubObject.OBJECT_ID == 0)
            {
                continue;
            }
            IngameMapObject objectInfo = new IngameMapObject();
            objectInfo.toolObjectId = baseSubObject.OBJECT_ID;
            objectInfo.objectPosition = new List<float>();
            objectInfo.objectPosition.Add(subObject.transform.position.x);
            objectInfo.objectPosition.Add(subObject.transform.position.y);
            objectInfo.objectPosition.Add(subObject.transform.position.z);

            objectInfo.objectScale = new List<float>();
            objectInfo.objectScale.Add(subObject.transform.localScale.x);
            objectInfo.objectScale.Add(subObject.transform.localScale.y);
            objectInfo.objectScale.Add(subObject.transform.localScale.z);
            objectInfo.toolSpriteName = baseSubObject.GetComponent<SpriteRenderer>().sprite.name;
            if(objectInfo.toolSpriteName.Contains("(Clone)"))
            {
                objectInfo.toolSpriteName = objectInfo.toolSpriteName.Replace("(Clone)", "");
            }
            objectInfo.objectCollisionSize = new List<float>();
            objectInfo.objectCollisionSize.Add(baseSubObject.GetComponent<BoxCollider2D>().size.x);
            objectInfo.objectCollisionSize.Add(baseSubObject.GetComponent<BoxCollider2D>().size.y);
            mapInfos.objectLists.Add(objectInfo);
        }

        if(mapInfos.objectLists.Count>0)
        {
            FileUtils.SaveFile<IngameMapObjectInfos>(createFilePath, $"{createFileName}.json", mapInfos);
        }
    }

    private List<JFoodObjectData> objectLists = null;
    public void CreateMapObject(int createId)
    {
        if(createTransform==null)
        {
            return;
        }

        GameObject createObject = IngameCreater.CreateFoodObject(createId);

        createObject.transform.parent = createTransform;
        JToolObjectData toolData = JsonDataManager.Instance.GetToolId(createId);
        BaseToolObject ojbect = createObject.GetComponent<BaseToolObject>();
        ojbect.GetComponent<SpriteRenderer>().sprite = SpriteManager.Instance.LoadSprite(toolData.IconFile);
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(MapDataCreater))]
public class MapToolCreateButton : Editor
{

    private int createObjecId ;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        MapDataCreater mapTool = (MapDataCreater)target;

        EditorGUILayout.Space();
        if(GUILayout.Button("Create MapFile"))
        {
            mapTool.CreateMapDataFile();
        }
        EditorGUILayout.Space();
        createObjecId = EditorGUILayout.IntField("ObjectId", createObjecId);
        if (GUILayout.Button("Create Object"))
        {
            mapTool.CreateMapObject(createObjecId);
        }
    }
}
#endif
