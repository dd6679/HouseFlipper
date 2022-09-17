using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_PlayerBehavior : MonoBehaviour
{
    public GameObject targetFactory;
    public GameObject customGrid;
    public float waitTime = 0.5f;
    public static bool isWaiting = false;

    GameObject moveTarget;
    Vector3 ScreenCenter;

    // 플레이어 행동 상태
    public enum PlayerBehaviorState
    {
        Idle,
        Move,
        Clean,
        Paint,
        Sell,
        BuildWall,
        BuildLintel,
        DemolishWall,
        WorkTileAndPanel,
    }

    public static PlayerBehaviorState behaviorState = PlayerBehaviorState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        moveTarget = Instantiate(targetFactory);
    }

    // Update is called once per frame
    void Update()
    {
        switch (behaviorState)
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
                break;
            case PlayerBehaviorState.BuildLintel:
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

    Outline outline;

    private void Move()
    {
        // 마우스 포지션을 취득해서 대입
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        NK_CustomGrid gridScript = customGrid.GetComponent<NK_CustomGrid>();

        // 마우스 포지션에서 레이를 던져 물체 감지 시 hit에 대입
        if (Physics.Raycast(ray, out var hit))
        {
            // 오브젝트의 타겟 취득
            if (hit.collider.gameObject.CompareTag("Furniture"))
            {
                outline = hit.collider.gameObject.GetComponent<Outline>();
                if (outline != null)
                    outline.enabled = true;
                GameObject go = hit.collider.gameObject;
                //moveTarget.transform.SetParent(go.transform, true);
                //moveTarget.transform.position = new Vector3(hit.transform.position.x, moveTarget.transform.position.y, hit.transform.position.z);
                moveTarget.transform.localScale = go.transform.localScale;
                gridScript.target = moveTarget;
                gridScript.structure = hit.collider.gameObject;
                isWaiting = true;
            }
            else
            {
                isWaiting = false;
                if (outline != null)
                    outline.enabled = false;
            }
            if (NK_UIController.isFinishWaiting)
            {
                moveTarget.SetActive(true);
                isWaiting = false;
            }
            /*else
            {
                gridScript.target = null;
                gridScript.structure = null;
                if (moveTarget != null)
                    moveTarget.SetActive(false);
            }*/

        }
    }

    private void Clean()
    {
        // 청소도구를 플레이어에게 쥐게 한다
        NK_ChangeTool.instance.SwitchTool((int)NK_ChangeTool.ToolState.CleanTool);
        // 청소하는 모션을 적용시킨다
        // - 맵에서 지저분한 물체가 청소도구와 닿으면 Destroy
    }

    private void Paint()
    {
        // 페인트롤을 플레이어에게 쥐게 한다
        NK_ChangeTool.instance.SwitchTool((int)NK_ChangeTool.ToolState.PaintTool);
        // 페인트하는 모션을 적용시킨다
        // - 맵에서 페인트롤과 닿으면 벽의 색이 바뀐다
    }

    private void Sell()
    {
        // 가격 측정 기기를 플레이어에게 쥐게 한다
        NK_ChangeTool.instance.SwitchTool((int)NK_ChangeTool.ToolState.SellTool);
        // 기기를 가구에 댄다
        // 가격이 나온다
    }

    private void DemolishWall()
    {
        // 망치를 플레이어에게 쥐게 한다
        NK_ChangeTool.instance.SwitchTool((int)NK_ChangeTool.ToolState.DemolishTool);
        // 부수는 모션을 적용시킨다
        // - 맵에서 망치와 닿으면 Demolish
    }

    // 상태 변화 관리 함수
    void ChangeState(PlayerBehaviorState state)
    {
        behaviorState = state;
        print(behaviorState + " 상태입니다.");
        if (moveTarget != null)
        {
            moveTarget.SetActive(false);
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

    // 인방보 올리기
    public void OnClickBuildLintel()
    {
        ChangeState(PlayerBehaviorState.BuildLintel);
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
}
