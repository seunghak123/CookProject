using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Seunghak.Common
{
    class CommonUtil
    {
        public static long PoolRemoveSecTime = 600;
        public static string StringToMD5(string str)
        {
            StringBuilder MD5Str = new StringBuilder();
            byte[] byteArr = Encoding.ASCII.GetBytes(str);
            byte[] resultArr = (new MD5CryptoServiceProvider()).ComputeHash(byteArr);

            for (int cnti = 0; cnti < resultArr.Length; cnti++)
            {
                MD5Str.Append(resultArr[cnti].ToString("X2"));
            }
            return MD5Str.ToString();
        }
        public static string MD5ToString(string md5str)
        {
            byte[] bytePassword = Encoding.UTF8.GetBytes(md5str);
            string emptymd5;

            using (MD5 md5 = MD5.Create())
            {
                byte[] byteHashedPassword = md5.ComputeHash(bytePassword);
                emptymd5 = byteHashedPassword.ToString();
            }
            return emptymd5;
        }
        public static Type GetTypeFromAssemblies(string TypeName)
        {
            var type = Type.GetType(TypeName);
            if (type != null)
                return type;

            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
            foreach (var assemblyName in referencedAssemblies)
            {
                var assembly = System.Reflection.Assembly.Load(assemblyName);
                if (assembly != null)
                {
                    type = assembly.GetType(TypeName);
                    if (type != null)
                        return type;
                }
            }

            return null;
        }
        #region PlayerPref
        public static void SavePlayerPref<T>(PlayerPrefKey saveKey, T saveData)
        {
            PlayerPrefs.SetString(saveKey.ToString(), saveData.ToString());

            PlayerPrefs.Save();
        }
        public static T GetPlayerPref<T>(PlayerPrefKey saveKey)
        {
            string getValue = PlayerPrefs.GetString(saveKey.ToString());
            T convertValue;
            try
            {
                convertValue = (T)Convert.ChangeType(getValue, typeof(T));
            }
            catch (Exception e)
            {
                Debug.Log($"ConvertValue Error {e.Message}");
                convertValue = default(T);
            }
            return convertValue;
        }
        private static string attendString = "SaveBundleHash";
        public static void SaveAssetBundleHash(string assetName, long saveValue)
        {
            string saveKey = $"{attendString}_{assetName}";
            PlayerPrefs.SetString(saveKey, saveValue.ToString());
        }
        public static long GetAssetBundleLocalHash(string assetName)
        {
            long returnValue = 0;
            string loadKey = $"{attendString}_{assetName}";
            string getValue = PlayerPrefs.GetString(loadKey);
            try
            {
                returnValue = (long)Convert.ChangeType(getValue, typeof(long));
            }
            catch (Exception e)
            {
                Debug.Log($"Convert HashValue Error {e.Message}");
            }
            return returnValue;
        }
        public static string GetTimerString(float timer)
        {
            string timerString = string.Empty;
            int hour = (int)timer / 3600;
            int mins = ((int)timer % 3600) / 60;
            int seconds = (int)timer % 60 ;
            timer = timer <= 0 ? 0 : timer;
            if (timer < 60.0f)
            {
                timerString = $"{timer.ToString("F2")}초" ;
            }
            else if(timer <3600.0f)
            {
                timerString = $"{mins}분{seconds}초";
            }
            else
            {
                timerString = $"{hour}시{mins}분";
            }

            return timerString;
        }
        #endregion PlayerPref
    }
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> keys = new List<TKey>();
        [SerializeField]
        private List<TValue> values = new List<TValue>();

        // save the dictionary to lists
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        // load dictionary from lists
        public void OnAfterDeserialize()
        {
            this.Clear();

            if (keys.Count != values.Count)
                throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

            for (int i = 0; i < keys.Count; i++)
                this.Add(keys[i], values[i]);
        }
    }
}
