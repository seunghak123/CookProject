using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class FireBaseDataBase : BaseDataBase
{
    DatabaseReference firebaseRootDB = null;

    private Dictionary<E_DATABASE_TYPE, DatabaseReference> firebaseDBs = new Dictionary<E_DATABASE_TYPE, DatabaseReference>();
    public void SaveDB(UserDataInfo saveData)
    {
        //saveData.userOption.FBXVolume
    }
    public UserDataInfo LoadDB()
    {
        UserDataInfo userDB = new UserDataInfo();

        return userDB;
    }
    public bool InitDB()
    {
        firebaseRootDB = FirebaseDatabase.DefaultInstance.RootReference;

        firebaseDBs[E_DATABASE_TYPE.USER_INFO] = FirebaseDatabase.DefaultInstance.RootReference.Child(E_DATABASE_TYPE.USER_INFO.ToString()).Child(UserDataManager.Instance.UserIDToken);

        for(int i=0;i< (int)E_DATABASE_TYPE.USER_OPTION;i++)
        {
            firebaseDBs[(E_DATABASE_TYPE)i] = FirebaseDatabase.DefaultInstance.RootReference.Child(E_DATABASE_TYPE.USER_ITEM.ToString()).
                Child(UserDataManager.Instance.UserIDToken);
        }
        firebaseDBs[E_DATABASE_TYPE.USER_ITEM] = FirebaseDatabase.DefaultInstance.RootReference.Child(E_DATABASE_TYPE.USER_ITEM.ToString()).Child(UserDataManager.Instance.UserIDToken);

        return true;
    }
    public void UpdateDB(UserDataInfo updateData)
    {

    }
}
