using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Seunghak.UIManager
{
    /// <summary>
    /// �ó����� �ϳ��ϳ��� ���� ������
    /// </summary>
    public class ScenarioData
    {
        public int scenarioDataID;

    }

    public class GameScenarioWindow : BaseUIWindow
    {
       [SerializeField] ScenarioScrollView scenarioScrollView;

        public override void EnterWindow()
        {
            base.EnterWindow();

            // �����͸� �������� ��ũ�Ѻ信 �־��־� ������ �սô�.

            scenarioScrollView.Init(); // �� �ó������� ���� ������ �ؾ� �ҵ�?
            // �� �ó������� ���� ������ �ʿ��ϸ�, �ִ� ��� ���̴��� Ȯ���Ͻð�,
            // �� �����Ϳ� ���� ������ ������ �� ���Ҹ� �ΰ� �ϸ� �� �� ������...?
        }

        public void EnterGameModeWindow()
        {

        }

        #region OnClick Event
        public void EnterStageWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.StageWindow);
        }
        #endregion

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

