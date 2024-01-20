using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class CharacterInfoData : CommonScrollItemData
    {
        public JCharacterData data;

        public CharacterInfoData(JCharacterData data)
        {
            this.data = data;
        }
    }

    public class CharacterSelectScrollView : OHScrollView
    {
        List<CharacterInfoData> characterDataList = new List<CharacterInfoData>();

        public void InitScrollView(List<JCharacterData> list)
        {

            if (characterDataList.Count == 0)
            {
                foreach (JCharacterData characterData in list)
                {
                    characterDataList.Add(new CharacterInfoData(characterData));
                }
            }

            base.InitScrollView<CharacterInfoData>(characterDataList);
        }
    }
}
