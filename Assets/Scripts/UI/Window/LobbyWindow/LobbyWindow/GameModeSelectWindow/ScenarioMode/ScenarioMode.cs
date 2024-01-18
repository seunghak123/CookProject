using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class ScenarioMode : BaseGameMode
    {
        [SerializeField] ScenarioChapterScrollView chapterScrollView;

        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            GameMode = E_GAMEMODE.ScenarioMode;

            chapterScrollView.InitScrollView(JsonDataManager.LoadJsonDatas<JChapterData>(E_JSON_TYPE.JChapterData).Values.ToList());

            return true;
        }
    }
}
    