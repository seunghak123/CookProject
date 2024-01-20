using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class ScenarioChapterData : CommonScrollItemData
    {
        public JChapterData data;

        public ScenarioChapterData(JChapterData data)
        {
            this.data = data;
        }
    }

    public class ScenarioChapterScrollView : OHScrollView
    {
        List<ScenarioChapterData> chapterDataList = new List<ScenarioChapterData>();

        public void InitScrollView(List<JChapterData> list)
        {

            if (chapterDataList.Count == 0)
            {
                foreach (JChapterData chapterData in list)
                {
                    chapterDataList.Add(new ScenarioChapterData(chapterData));
                }
            }

            base.InitScrollView<ScenarioChapterData>(chapterDataList);
        }
    }
}
