using System;
using UnityEngine;
namespace Seunghak.SceneManager
{
    using Seunghak.Common;
    using Seunghak.UIManager;
    using System.Collections.Generic;

    public class IngameSceneData : SceneDeliverData
    {
        public int stageId;
        public int currentCharacterId;
        public List<string> userKey;

        public IngameSceneData(int stageId, int currentCharacterId, List<string> userKey)
        {
            this.stageId = stageId;
            this.currentCharacterId = currentCharacterId;
            this.userKey = userKey;
        }
    }
    public class IngameSceneController : SceneController
    {
        [SerializeField] private IngameCanvas ingameUICanvas;
        protected override void Awake()
        {
            base.Awake();
        }
        //���� �� ��Ʈ�ѷ� �ʱ�ȭ �Լ�
        public override void InitSceneController() 
        {
            IngameSceneData sceneData = SceneManager.Instance.CurDeliverData as IngameSceneData;
            //sceneData.stageId
            //
            UIManager.Instance.CloseUI();

            //테스트 코드 SceneController에 데이터 넘기고 그걸 인게임 매니저에 전달 필요
            //UserDataManager.Instance.

            //인게임 타입마다 DeliverData를 형변환해서 IngameManager에게 넘겨주기

            IngameManager.currentManager.CreateGame(0);

            //선택한 덱 가져오기 , 유저 데이터 세팅
            //스킬 파티클 선행 생성
            

            //유저 데이터 가져와주고
            ingameUICanvas.InitIngameCanvas();
            //로비씬으로 변경시 현재 데이터 보내주고
        }
        //DataType
        //public 
        public SceneDeliverData GetSceneData()
        {
            return sceneLoadData;
        }
        //�� �ε� �� �� �Ŵ����� ���� ��Ʈ�ѷ� ����ϴ� �Լ�
        public override void RegistSceneController()
        {
            base.RegistSceneController();
        }
        public override void RegistSceneStepAction(E_SCENESTEP_TYPE actionType, Action playAction)
        {
            base.RegistSceneStepAction(actionType, playAction);
        }
    }
}
