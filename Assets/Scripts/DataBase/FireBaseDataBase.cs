using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Newtonsoft.Json;

public class FireBaseDataBase : BaseDataBase
{
    DatabaseReference firebaseRootDB = null;

    private Dictionary<E_DATABASE_TYPE, DatabaseReference> firebaseDBs = new Dictionary<E_DATABASE_TYPE, DatabaseReference>();
    public void SaveDB(UserDataInfo saveData)
    {
        firebaseDBs[E_DATABASE_TYPE.USER_INFO].SetRawJsonValueAsync(JsonConvert.SerializeObject(saveData.userInfoData));

        firebaseDBs[E_DATABASE_TYPE.USER_ITEM].SetRawJsonValueAsync(JsonConvert.SerializeObject(saveData.userItemDatas));

        firebaseDBs[E_DATABASE_TYPE.USER_STAGE].SetRawJsonValueAsync(JsonConvert.SerializeObject(saveData.userStoryDatas));
        CommonUtil.SavePlayerPref<UserOptionData>(PlayerPrefKey.UserOptionData, saveData.userOption);
    }
    public UserDataInfo LoadDB()
    {
        UserDataInfo userDB = new UserDataInfo();

        firebaseDBs[E_DATABASE_TYPE.USER_INFO].GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapShot = task.Result;
                userDB.userInfoData = JsonConvert.DeserializeObject<UserInfoData>(snapShot.GetRawJsonValue());

            }
        });

        firebaseDBs[E_DATABASE_TYPE.USER_STAGE].GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapShot = task.Result;
                userDB.userStoryDatas = JsonConvert.DeserializeObject<UserStoryDatas>(snapShot.GetRawJsonValue());

            }
        });
        firebaseDBs[E_DATABASE_TYPE.USER_ITEM].GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapShot = task.Result;
                userDB.userItemDatas = JsonConvert.DeserializeObject<UserItemDatas>(snapShot.GetRawJsonValue());
            }
        });

        userDB.userOption = CommonUtil.GetPlayerPref<UserOptionData>(PlayerPrefKey.UserOptionData);

        return userDB;
    }
    public bool InitDB()
    {
        firebaseRootDB = FirebaseDatabase.DefaultInstance.RootReference;

        for(int i=0;i< (int)E_DATABASE_TYPE.USER_OPTION;i++)
        {
            firebaseDBs[(E_DATABASE_TYPE)i] = FirebaseDatabase.DefaultInstance.RootReference.Child(((E_DATABASE_TYPE)i).ToString()).
                Child(UserDataManager.Instance.UserIDToken);
        }

        return true;
    }
    public void UpdateDB(UserDataInfo updateData)
    {
        //우선 이걸로 테스트
        firebaseDBs[E_DATABASE_TYPE.USER_INFO].SetRawJsonValueAsync(JsonConvert.SerializeObject(updateData.userItemDatas));

        firebaseDBs[E_DATABASE_TYPE.USER_ITEM].SetRawJsonValueAsync(JsonConvert.SerializeObject(updateData.userItemDatas));

        firebaseDBs[E_DATABASE_TYPE.USER_STAGE].SetRawJsonValueAsync(JsonConvert.SerializeObject(updateData.userStoryDatas));

        CommonUtil.SavePlayerPref<UserOptionData>(PlayerPrefKey.UserOptionData, updateData.userOption);
    }
}
