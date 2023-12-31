using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class FireBaseDataBase : BaseDataBase
{
    DatabaseReference firebaseRootDB = null;
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

        return true;
    }
    public void UpdateDB(UserDataInfo updateData)
    {

    }
}
