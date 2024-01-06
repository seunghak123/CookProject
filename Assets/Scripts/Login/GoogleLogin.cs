using UnityEngine;

namespace Seunghak.LoginSystem
{
    using System;
#if UNITY_ANDROID || UNITY_IOS
    using GooglePlayGames;
    using Seunghak.Common;

    public class GoogleLogin :  LoginInterface
    {
        private Action loginSuccessAction = null;
        public void InitLogin()
        {
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }
        public void PlatformLogin(Action successAction)
        {
            loginSuccessAction = successAction;
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }

        internal void ProcessAuthentication(GooglePlayGames.BasicApi.SignInStatus status)
        {
            if (status == GooglePlayGames.BasicApi.SignInStatus.Success)
            {
                GoogleLoginSuccess();
            }
            else
            {
                GoogleLoginFail();
            }
        }
        private void GoogleLoginSuccess()
        {
            loginSuccessAction();
            UserDataManager.Instance.SetUserToken(PlayGamesPlatform.Instance.GetUserId());
        }
        private void GoogleLoginFail()
        {
            Debug.Log("LoginFaile");
        }

        public void LogOut()
        {
            PlayGamesPlatform.Activate();
        }
    }
#endif
}

