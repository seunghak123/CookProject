using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SceneManager에 두는게 아니라 Util쪽에 enum으로 씬 종류 나열
public enum E_SCENE_TYPE
{
    INTRO,
    TITLE,
    LOBBY,
    INGAME,
    FIELD,
    END,
}
public enum E_SCENESTEP_TYPE
{
    NONE,
    EXITSTEP,
    LOADSTEP,
    OPENSTEP,
    END,
}

namespace Seunghak.SceneManager
{
    public class SceneManager : UnitySingleton<SceneManager>
    {
        LoadingPopup loadingPopupUI; // 임시

        // ------------------------테스트-------------------------------- 
        [SerializeField] private E_SCENE_TYPE testSceneType = E_SCENE_TYPE.INTRO;
        [SerializeField] private bool isWatingSuccessSequence = false;
        // ------------------------테스트-------------------------------- 
        #region Property
        private Dictionary<E_SCENESTEP_TYPE, Action> addactionList = new Dictionary<E_SCENESTEP_TYPE, Action>();
        private Dictionary<E_SCENESTEP_TYPE, Action> baseactionList = new Dictionary<E_SCENESTEP_TYPE, Action>();
        private E_SCENESTEP_TYPE currentStepType = E_SCENESTEP_TYPE.NONE;
        private E_SCENE_TYPE currentSceneType = E_SCENE_TYPE.INTRO;
        private E_SCENE_TYPE nextSceneType = E_SCENE_TYPE.INTRO;
        private SceneController currentSceneController = null;
        private SceneDeliverData currentDeliverData = null;

        public SceneDeliverData CurDeliverData
        {
            get { return currentDeliverData; }
            set 
            { 
                if(value!=null)
                {
                    currentDeliverData = value;
                }
            }
        }
        #endregion Property

        protected override void InitSingleton()
        {
            InitSceneManager();
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                ChangeScene(testSceneType);
            }
        }
        // ------------------------테스트--------------------------------//
        #region UtilityLogic
        public void RegistSceneController(SceneController addedSceneController)
        {
            currentSceneController = addedSceneController;
        }
        public void AddStepAction(E_SCENESTEP_TYPE addStepType, Action addAction)
        {
            if (addactionList.ContainsKey(addStepType))
            {
                addactionList[addStepType] = addAction;
            }
            else
            {
                addactionList.Add(addStepType, addAction);
            }
        }
        //공용으로 사용할 씬 나갈때 스텝 
        public void ChangeScene(E_SCENE_TYPE nextScene)
        {
            nextSceneType = nextScene;
            NextStep(E_SCENESTEP_TYPE.EXITSTEP);
        }
        //베이스가 되는 씬 기반으로 돌아가는 함수
        public static void GoToBase()
        {
            //로비 씬 로드 하고 기본 베이스 화면으로 돌아가는 작업
        }
        #endregion UtilityLogic
        #region BasicLogic
        private void InitSceneManager()
        {
            baseactionList.Add(E_SCENESTEP_TYPE.EXITSTEP, BaseExitStep);
            baseactionList.Add(E_SCENESTEP_TYPE.LOADSTEP, BaseLoadStep);
            baseactionList.Add(E_SCENESTEP_TYPE.OPENSTEP, BaseOpenStep);
        }
        private void BaseExitStep()
        {
            currentSceneController = null;
            ExecuteAddStepAcion();
            NextStep(E_SCENESTEP_TYPE.LOADSTEP);
        }
        private void BaseLoadStep()
        {
            StartCoroutine(LoadData());
        }
        private void BaseOpenStep()
        {
            ExecuteAddStepAcion();
            NextStep(E_SCENESTEP_TYPE.END);
        }
        private WaitForEndOfFrame waitframe = new WaitForEndOfFrame();
        private IEnumerator LoadData()
        {
            loadingPopupUI = UIManager.UIManager.Instance.PushUI(UIManager.UI_TYPE.LoadingPopup) as LoadingPopup;

            ExecuteAddStepAcion();
            AsyncOperation taskLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextSceneType.ToString());
            while (!taskLoad.isDone || isWatingSuccessSequence)
            {
                //m_TaskLoad.progress progress 표시 또는
                yield return waitframe;
            }
            while (currentSceneController == null)
            {
                yield return waitframe;
            }
            //여기에 데이터 로드 할떄까지 대기
            //씬 로드시 완료될떄까지 대기탄다
            NextStep(E_SCENESTEP_TYPE.OPENSTEP);
            yield break;
        }
        private void ExecuteStepAction()
        {
            if (baseactionList.ContainsKey(currentStepType))
            {
                baseactionList[currentStepType]();
            }
        }
        private void ExecuteAddStepAcion()
        {
            if (addactionList.ContainsKey(currentStepType))
            {
                addactionList[currentStepType]();

                addactionList.Remove(currentStepType);
            }
        }
        private void NextStep(E_SCENESTEP_TYPE nextStepType)
        {
            if (nextStepType == E_SCENESTEP_TYPE.END)
            {
                //모든 씬 로드 액션 끝
                currentStepType = E_SCENESTEP_TYPE.NONE;
                currentSceneType = nextSceneType;
                //로딩 화면 종료
                if(loadingPopupUI!=null)
                {
                    loadingPopupUI.CloseUI();
                }
                currentSceneController.SetSceneData(currentDeliverData);
                currentSceneController.InitSceneController();
                //각 씬 컨트롤러 여기서 초기화 함수 호출 씬 컨트롤러는 BaseSceneController 상속 받는다.
                // 각 씬에서 반드시 해줘야 할 행동 리스트들을 나열해놓은 인터페이스를 상속받는다
                //인터페이스 상속 받은것을 찾아서 실행만 시켜주면 된다.
                return;
            }
            currentStepType = nextStepType;
            ExecuteStepAction();
        }
        #endregion BasicLogic
    }
}
