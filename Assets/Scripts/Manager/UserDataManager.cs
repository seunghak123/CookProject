using Seunghak.LoginSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Seunghak.Common
{
    public enum PlayerPrefKey
    {
        SaveTest,
        UserData,
        UserToken,


        /// <summary>
        /// Unity Editor일때 저장하는타입 용
        /// </summary>
        UserInfoData = 3000,
        UserOptionData,
        UserItemDatas,
        UserStoryDatas,
        UserLobbyDatas,
    }
    public class UserDataManager : UnitySingleton<UserDataManager>
    {
        private UserDataInfo userDataInfo = new UserDataInfo();
        private LoginInterface userLogin;
        private BaseDataBase userDataBase;
        private string userIDToken = "";
        public string UserIDToken
        {
            get
            {
                return userIDToken;
            }
        }
        public void SetLoginInfo(E_LOGIN_TYPE loginType)
        {
            switch (loginType)
            {
                case E_LOGIN_TYPE.GUEST_LOGIN:
                    userLogin = new GuestLogin();
                    break;
#if UNITY_ANDROID || UNITY_IOS
                case E_LOGIN_TYPE.GOOGLE_LOGIN:
                    userLogin = new GoogleLogin();
                    break;
                case E_LOGIN_TYPE.APPLE_LOGIN:
                    userLogin = new AppleLogin();
                    break;
#endif
            }
            userDataInfo.userInfoData.userLoginType = loginType;
            userLogin.InitLogin();

            SetDataBaseInfo();
        }
        public void SetDataBaseInfo()
        {
#if !UNITY_EDITOR
            userDataBase = new LocalDataBase();
#else
            userDataBase = new FireBaseDataBase();
#endif
        }
        public void LoginPlatform(Action successResultAction)
        {
            userLogin.PlatformLogin(successResultAction);
        }
        public int GetUserItemCount(int id)
        {
            return 0;
        }
        public void SetUserToken(string userToken)
        {
            userIDToken = userToken;

            userDataBase.InitDB();
        }
        public bool AutoLogin()
        {
            userIDToken = CommonUtil.GetPlayerPref<string>(PlayerPrefKey.UserToken);
            if(string.IsNullOrEmpty(userIDToken))
            {
                return false;
            }
            //userIDToken 으로 서버쪽에 로그인 메소드 보내고

            return true;
        }
        public void AddUserItem()
        {
            userDataInfo.userItemDatas.AddItem(1, 10000);
        }
        public void SetUserStoryInfo(StoryEndData info)
        {
            ScenarioStageResultInfo resultInfo;
            if (userDataInfo.userStoryDatas.scenarioStageResults.ContainsKey(info.stageData.ID))
            {
                resultInfo = userDataInfo.userStoryDatas.scenarioStageResults[info.stageData.ID];
            }
            else
            {
                resultInfo = new ScenarioStageResultInfo();
            }
            resultInfo.storyId = info.stageData.ID;
            resultInfo.maxScore = resultInfo.maxScore > info.stageScore ? resultInfo.maxScore : info.stageScore;

            userDataInfo.userStoryDatas.scenarioStageResults[resultInfo.storyId] = resultInfo;
        }
        public UserLobbyInfoData GetUserLobbyInfoData()
        {
            return userDataInfo.userLobbyInfoDatas;
        }
        public UserItemDatas GetUserItemData()
        {
            return userDataInfo.userItemDatas;
        }
        public UserOptionData GetUserOptionData()
        {
            return userDataInfo.userOption;
        }
        public ScenarioStageResultInfo GetScenarioStageResultData(int stageID)
        {
            if (userDataInfo.userStoryDatas.scenarioStageResults.TryGetValue(stageID, out ScenarioStageResultInfo data))
                return data;

            return null;
        }
        public bool IsScenarioStageResult(int stageID)
        {
            if (stageID == 0)
                return true;

            return userDataInfo.userStoryDatas.scenarioStageResults.ContainsKey(stageID);
        }
#region DB_SAVE
        /// <summary>
        /// 해당 내용들은 로컬 DB를 사용할 경우에만 해당한다
        /// 하나는 PlayerPref를 이용 차후엔 확장해서 직접적으로 호출하는게 아닌 
        /// DB매니저를 통해서 어떤 DB를 통하든 특정 인터페이스를 상속받은 DB의 행동을 실행하도록한다.
        /// </summary>
        public void SaveUserData()
        {
            userDataBase.SaveDB(userDataInfo);
        }
        public void LoadUserData()
        {
            userDataInfo = userDataBase.LoadDB();
        }
#endregion DB_SAVE 
    }
    [Serializable]
    public class UserItemDatas
    {
        public Dictionary<int, long> itemDic = new Dictionary<int, long>();
        public bool AddItem(int id, long itemCount)
        {
            if ((int)E_ITEM_TYPE.CRYSTALS == id || (int)E_ITEM_TYPE.VALID_CRYSTALS == id)
            {
                long useritemCount = itemDic[(int)E_ITEM_TYPE.CRYSTALS] + itemDic[(int)E_ITEM_TYPE.VALID_CRYSTALS];

                if (itemCount > 0)
                {
                    itemDic[id] += itemCount;
                    return true;
                }
                else
                {
                    long remainCount = 0;
                    if (useritemCount + itemCount > 0)
                    {
                        itemDic[(int)E_ITEM_TYPE.VALID_CRYSTALS] -= itemCount;
                        if (itemDic[(int)E_ITEM_TYPE.VALID_CRYSTALS] < 0)
                        {
                            remainCount = itemDic[(int)E_ITEM_TYPE.VALID_CRYSTALS];
                            itemDic[(int)E_ITEM_TYPE.VALID_CRYSTALS] = 0;
                        }
                        itemDic[(int)E_ITEM_TYPE.CRYSTALS] -= remainCount;

                        return true;
                    }
                    return false;
                }

            }
            else
            {
                if (itemDic[id] + itemCount > 0)
                {
                    itemDic[id] += itemCount;
                    return true;
                }
                return false;
            }
        }
        public long GetItemCount(E_ITEM_TYPE itemType)
        {
            int id = (int)itemType;
            return GetItemCount(id);
        }
        public long GetItemCount(int id)
        {
            if (!itemDic.ContainsKey(id))
            {
                itemDic[id] = 0;
            }

            if ((int)E_ITEM_TYPE.CRYSTALS == id || (int)E_ITEM_TYPE.VALID_CRYSTALS == id)
            {
                if (!itemDic.ContainsKey((int)E_ITEM_TYPE.CRYSTALS))
                {
                    itemDic[(int)E_ITEM_TYPE.CRYSTALS] = 0;
                }

                if (!itemDic.ContainsKey((int)E_ITEM_TYPE.VALID_CRYSTALS))
                {
                    itemDic[(int)E_ITEM_TYPE.VALID_CRYSTALS] = 0;
                }

                return itemDic[(int)E_ITEM_TYPE.CRYSTALS] + itemDic[(int)E_ITEM_TYPE.VALID_CRYSTALS];
            }

            return itemDic[id];
        }
    }
    /// <summary>
    /// 유저 플랫폼, 로그인, 
    /// </summary>
    [Serializable]
    public class UserInfoData
    {
        public string userKey = string.Empty;
        public string userCurrentLoginTime = string.Empty;
        public bool isLogined = false;
        public E_LOGIN_TYPE userLoginType = E_LOGIN_TYPE.GUEST_LOGIN;
        public int platformType = 0;
        public string userNickName = string.Empty;
        public string userUserEmail = string.Empty;
    }
    [Serializable]
    public class UserLobbyInfoData
    {
        public int userCharacterCurrentId = 1;
    }
    /// <summary>
    /// 유저 옵션 데이터
    /// </summary>
    [Serializable]
    public class UserOptionData
    {
        //로컬 저장예정
        [Range(0, 100)] public int masterVolume = 100;
        [Range(0, 100)] public int soundVolume = 100;
        [Range(0, 100)] public int fbxVolume = 100;
        public E_LANGUAGE_TYPE userLangType = E_LANGUAGE_TYPE.KOREAN;
        public int MasterVolume { get { return masterVolume; } set { masterVolume = value; } }
        public int SoundVolume { get { return soundVolume; } set { soundVolume = value; } }
        public int FBXVolume { get { return fbxVolume; } set { fbxVolume = value; } }
        public bool IsMute { get; set; } = false;
        public E_LANGUAGE_TYPE UserLanguageType { get { return userLangType; }set { userLangType = value; } }
    }
    /// <summary>
    /// 유저 스토리 데이터
    /// </summary>
    [Serializable]
    public class ScenarioStageResultInfo
    {
        public int storyId;
        public int maxScore;
    }
    [Serializable]
    public class UserStoryDatas
    {
        public Dictionary<int, ScenarioStageResultInfo> scenarioStageResults = new Dictionary<int, ScenarioStageResultInfo>();
    }
    /// <summary>
    /// 유저 데이터 베이스 저장용 구조
    /// 필요한 경우에는 UserData 클래스 종류를 늘려줄 것
    /// </summary>
    public class UserDataInfo
    {
        public UserInfoData userInfoData = new UserInfoData();
        public UserOptionData userOption = new UserOptionData();
        public UserItemDatas userItemDatas = new UserItemDatas();
        public UserStoryDatas userStoryDatas = new UserStoryDatas();
        public UserLobbyInfoData userLobbyInfoDatas = new UserLobbyInfoData();
    }
}