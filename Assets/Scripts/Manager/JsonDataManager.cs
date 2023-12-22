using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.Common
{
    public class JsonDataManager : UnitySingleton<JsonDataManager>
    {
        private static Dictionary<string,string> dicJsonData = new Dictionary<string, string>();

        public static Dictionary<string,T> LoadJsonDatas<T>(E_JSON_TYPE loadType) where T : JBaseData
        {
            String loadPath = "";
            String loadTypeString = loadType.ToString();

            if (loadTypeString.Contains("Data"))
            {
                loadTypeString = loadTypeString.Replace("Data", "");
            }
            if (dicJsonData.ContainsKey(loadType.ToString()))
            {
                return JsonConvert.DeserializeObject<Dictionary<string,T>> (dicJsonData[loadType.ToString()]) ;
            }

            Dictionary<string, T> loadedObject = new Dictionary<string, T>();
#if UNITY_EDITOR
            loadTypeString = loadTypeString + ".json";
            loadPath = FileUtils.JSONFILE_LOAD_PATH + loadTypeString.ToLower();
            object loadData = FileUtils.LoadFile<object>(loadPath);

            loadedObject = JsonConvert.DeserializeObject<Dictionary<string, T>>(loadData.ToString());

            if (!dicJsonData.ContainsKey(loadType.ToString()))
            {
                dicJsonData[loadType.ToString()] = loadData.ToString();
            }
#else
            UnityEngine.Object loadObject = GameResourceManager.Instance.LoadObject(loadTypeString.ToLower());
            loadedObject = JsonConvert.DeserializeObject<Dictionary<string,T>>(loadObject.ToString());

            if (!dicJsonData.ContainsKey(loadType.ToString()))
            {
                dicJsonData[loadType.ToString()] = loadObject.ToString();
            }
#endif
            return loadedObject;
        }
     
        public T GetSingleData<T>(int singleId, E_JSON_TYPE type) where T : JBaseData
        {
            string findKey = singleId.ToString();

            return GetSingleData<T>(findKey, type);
        }
        public T GetSingleData<T>(string singleKey, E_JSON_TYPE type) where T : JBaseData
        {
            Dictionary<string, T> dicDatas = LoadJsonDatas<T>(type);

            if(dicDatas.ContainsKey(singleKey))
            {
                return dicDatas[singleKey];
            }

            return null;
        }

        public JStageData GetStageData(int stageId)
        {
            Dictionary<string, JStageData> stageDatas = LoadJsonDatas<JStageData>(E_JSON_TYPE.JStageData);

            JStageData stageData = stageDatas[stageId.ToString()];

            return stageData;
        }

        public List<JFoodObjectData> GetFoodObjectLists()
        {
            return new List<JFoodObjectData>( LoadJsonDatas<JFoodObjectData>(E_JSON_TYPE.JFoodObjectData).Values); 
        }
        public JRecipeData GetRecipeData(int recipeId)
        {
            Dictionary<string, JRecipeData> recipeDatas = LoadJsonDatas<JRecipeData>(E_JSON_TYPE.JRecipeData);

            JRecipeData recipeData = recipeDatas[recipeId.ToString()];

            return recipeData;
        }
        public List<JRecipeData> GetOutputRecipeDatas(int objectId)
        {
            Dictionary<string, JRecipeData> recipeDatas = LoadJsonDatas<JRecipeData>(E_JSON_TYPE.JRecipeData);

            List<JRecipeData> outputDatas = new List<JRecipeData>(recipeDatas.Values).FindAll(find => find.UseObject == objectId);

            return outputDatas;
        }
    }
}