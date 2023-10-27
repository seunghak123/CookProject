﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SignInWithApple;

namespace Seunghak.LoginSystem
{
    public class AppleLogin : SignInWithApple, LoginInterface
    {
        private Action loginSuccessAction = null;
        private string appleUserId;

        public void InitLogin()
        {
            //Do nothing
        }
        public void PlatformLogin(Action successResultAct)
        {
            loginSuccessAction = successResultAct;
            Login(OnLogin);
        }
        private void OnLogin(SignInWithApple.CallbackArgs args)
        {
            if (args.error != null)
            {
                return;
            }

            UserInfo userInfo = args.userInfo;

            // Save the userId so we can use it later for other operations.
            appleUserId = userInfo.userId;

            GetCredentialState(appleUserId, OnCredentialState);
        }

        private void OnCredentialState(SignInWithApple.CallbackArgs args)
        {
            
            if (args.error != null)
            {
                //에러 발생
                //에러 state에따라 애플 로그인 처리
            }
            else
            {
                if (loginSuccessAction != null)
                {
                    loginSuccessAction();
                }
                //로그인 성공 및 서버 (현재는 sqlite) 에 계정 시리얼 번호 요청및 검증
            }
        }
    }
}