using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    /// <summary>
    /// 게임 모드 프리팹과 동일한 이름을 가지고 있어야 함
    /// </summary>
    public enum E_GAMEMODE
    {
        Unknown = 0,
        ScenarioMode = 1,
        TestMode = 2,
    }

    public class GameModeSelectWindow : BaseUIWindow
    {
        [SerializeField] Transform ContentsAreaTr;

        Dictionary<E_GAMEMODE, BaseGameMode> gameModeDic = new Dictionary<E_GAMEMODE, BaseGameMode>();

        E_GAMEMODE currGameMode = E_GAMEMODE.Unknown;
        
        /// <summary>
        /// E_GAMEMODE의 넘버를 받아서 해당하는 GameMode를 켜주는 메서드
        /// </summary>
        public void OnClickGameModeSelect(int gameModeNum)
        {
            if (gameModeNum == 0)
                Debug.LogError("gameModeNum에 0이 들어옴 E_GAMEMODE 열거형 확인요망");

            if ((int)currGameMode == gameModeNum)
                return;

            E_GAMEMODE gameMode = (E_GAMEMODE)gameModeNum;
            currGameMode = gameMode;
            BaseGameMode baseGameMode;

            if (gameModeDic.ContainsKey(gameMode) && gameModeDic[gameMode] != null)
            {
                baseGameMode = gameModeDic[gameMode];
                baseGameMode.Open();
            }
            else
            {
                if(gameModeDic.ContainsKey(gameMode))
                    gameModeDic.Remove(gameMode);

                baseGameMode = GameResourceManager.Instance.SpawnObject(gameMode.ToString()).GetComponent<BaseGameMode>();
                gameModeDic.Add(gameMode, baseGameMode);

                baseGameMode.Open(ContentsAreaTr);
            }

            foreach(BaseGameMode gm in gameModeDic.Values)
                if(gm.GameMode != gameMode)
                    gm.Close();
        }

        public override void EnterWindow()
        {
            base.EnterWindow();

            currGameMode = E_GAMEMODE.Unknown;
            OnClickGameModeSelect((int)E_GAMEMODE.ScenarioMode);
        }

        public override void ExitWindow()
        {
            base.ExitWindow();
        }

        public override void RestoreWindow()
        {
            base.RestoreWindow();
        }

        public override void StartWindow()
        {
            base.StartWindow();
        }
        public override void RegistEvent()
        {
            base.RegistEvent();
        }
        public override void DeleteRegistedEvent()
        {
            base.DeleteRegistedEvent();
        }
    }
}

