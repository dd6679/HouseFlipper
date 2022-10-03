using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_PlayerBehavior : MonoBehaviourPun
{
    //public GameObject targetFactory;
    //public GameObject customGrid;
    public float waitTime = 0.5f;
    public bool isWaiting = false;
    public GameObject Wall;

    Vector3 ScreenCenter;

    NK_ChangeTool changeTool;

    // 플레이어 행동 상태
    public enum PlayerBehaviorState
    {
        Idle,
        Move,
        Clean,
        Paint,
        Sell,
        BuildWall,
        DemolishWall,
        WorkTileAndPanel,
        Flame
    }

    public PlayerBehaviorState behaviorState = PlayerBehaviorState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        changeTool = GetComponent<NK_ChangeTool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (behaviorState == NK_PlayerBehavior.PlayerBehaviorState.Move)
        {
            // 마우스 포지션을 취득해서 대입
            Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
            //NK_CustomGrid gridScript = customGrid.GetComponent<NK_CustomGrid>();

            // 마우스 포지션에서 레이를 던져 물체 감지 시 hit에 대입
            if (Physics.Raycast(ray, out var hit))
            {
                // 오브젝트의 타겟 취득
                if (hit.collider.gameObject.CompareTag("Furniture"))
                {
                    if (hit.collider.gameObject.GetComponent<Outline>() != null)
                    {
                        outline = hit.collider.gameObject.GetComponent<Outline>();
                        outline.enabled = true;
                    }

                    if (hit.collider.gameObject.GetComponent<NK_Move>() != null)
                    {
                        nK_Move = (NK_Move)hit.collider.gameObject.GetComponent<NK_Move>();
                    }

                    isWaiting = true;
                }
                else
                {
                    InitializationMove();
                }
                // 게이지가 모두 차면 옮기기 기능 활성화
                if (NK_UIController.isFinishWaiting)
                {
                    nK_Move.enabled = true;
                    isWaiting = false;
                }
            }

        }
        else
        {
            InitializationMove();
        }
    }

    private void InitializationMove()
    {
        isWaiting = false;
        if (outline != null)
            outline.enabled = false;
        if (nK_Move != null)
            nK_Move.enabled = false;
        NK_UIController.isFinishWaiting = false;
    }

    Outline outline;
    NK_Move nK_Move;

    private void Move()
    {
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.Move);
    }

    private void Clean()
    {
        // 청소도구를 플레이어에게 쥐게 한다
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.CleanTool);
        // 마우스 클릭 중일 때 청소하는 모션을 적용시킨다
        // - 맵에서 지저분한 물체가 청소도구와 닿으면 Destroy
    }

    private void Paint()
    {
        // 페인트롤을 플레이어에게 쥐게 한다
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.PaintTool);
        // 페인트하는 모션을 적용시킨다
        // - 맵에서 페인트롤과 닿으면 벽의 색이 바뀐다
    }

    private void Sell()
    {
        // 가격 측정 기기를 플레이어에게 쥐게 한다
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.SellTool);
        // 기기를 가구에 댄다
        // 가격이 나온다
    }

    private void DemolishWall()
    {
        // 망치를 플레이어에게 쥐게 한다
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.DemolishTool);
        // 부수는 모션을 적용시킨다
        // - 맵에서 망치와 닿으면 Demolish
    }
    RaycastHit hit;
    private void BuildWall()
    {
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.Move);
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag.Contains("Floor"))
            {
                GameObject gogo = PhotonNetwork.Instantiate("Build", hit.point, Quaternion.identity);
                gogo.transform.position = hit.point;
                //UI.SetActive(false);
            }
        }

    }

    private void Flame()
    {
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.FlameTool);
    }

    // 상태 변화 관리 함수
    void ChangeState(PlayerBehaviorState state)
    {
        if (!photonView.IsMine)
            return;
        photonView.RPC("RpcChangeState", RpcTarget.All, state);
    }

    [PunRPC]
    public void RpcChangeState(PlayerBehaviorState state)
    {
        if (behaviorState == state) return;

        behaviorState = state;
        print(behaviorState + " 상태입니다.");

        switch (state)
        {
            case PlayerBehaviorState.Idle:
                break;
            case PlayerBehaviorState.Move:
                Move();
                break;
            case PlayerBehaviorState.Clean:
                Clean();
                break;
            case PlayerBehaviorState.Paint:
                Paint();
                break;
            case PlayerBehaviorState.Sell:
                Sell();
                break;
            case PlayerBehaviorState.BuildWall:
                BuildWall();
                break;
            case PlayerBehaviorState.Flame:
                Flame();
                break;
            case PlayerBehaviorState.DemolishWall:
                DemolishWall();
                break;
            case PlayerBehaviorState.WorkTileAndPanel:
                break;
            default:
                break;
        }
    }

    // 옮기기
    public void OnClickMove()
    {
        ChangeState(PlayerBehaviorState.Move);
    }

    // 청소하기
    public void OnClickClean()
    {
        ChangeState(PlayerBehaviorState.Clean);
    }

    // 칠하기
    public void OnClickPaint()
    {
        ChangeState(PlayerBehaviorState.Paint);
    }

    // 팔기
    public void OnClickSell()
    {
        ChangeState(PlayerBehaviorState.Sell);
    }

    // 벽 올리기
    public void OnClickBuildWall()
    {
        ChangeState(PlayerBehaviorState.BuildWall);
    }

    // 화염방사기
    public void OnClickFlame()
    {
        ChangeState(PlayerBehaviorState.Flame);
    }

    // 벽 철거하기
    public void OnClickDemolishWall()
    {
        ChangeState(PlayerBehaviorState.DemolishWall);
    }

    // 타일 및 패널작업하기
    public void OnClickWorkTileAndPanel()
    {
        ChangeState(PlayerBehaviorState.WorkTileAndPanel);
    }

    [PunRPC]
    private void RpcProgressBar(int viewId)
    {
        PhotonView view = PhotonView.Find(viewId);
        if (view != null)
        {
            Image progress = view.gameObject.GetComponent<Image>();
            progress.fillAmount += 0.03f;
        }
    }

    [PunRPC]
    private void RpcCompleteStoreQuest(string quest, string location)
    {
        NK_QuestUI.completeQuest[location].Add(quest);
        NK_QuestUI.quests[location][quest] = 100;
    }
}
