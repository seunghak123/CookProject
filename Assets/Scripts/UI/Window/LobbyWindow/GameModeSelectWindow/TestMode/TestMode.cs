using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class TestMode : BaseGameMode
    {
        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            GameMode = E_GAMEMODE.TestMode;

            // TODO

            return true;
        }
    }

}
