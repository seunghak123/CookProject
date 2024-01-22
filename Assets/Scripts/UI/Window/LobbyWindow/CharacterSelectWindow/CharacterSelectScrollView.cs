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
        List<CharacterInfoItem> characterInfoItemList = new List<CharacterInfoItem>();

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

            // characterInfoItemList 초기화
            characterInfoItemList.Clear();
            foreach (RectTransform rect in itemList)
            {
                CharacterInfoItem characterInfoItem = rect.gameObject.GetComponent<CharacterInfoItem>();
                characterInfoItemList.Add(characterInfoItem);
            }
        }

        public void UpdateScrollViewInfo()
        {
            foreach(CharacterInfoItem item in characterInfoItemList)
            {
                item.SetCharaterData();
            }
        }
    }
}
