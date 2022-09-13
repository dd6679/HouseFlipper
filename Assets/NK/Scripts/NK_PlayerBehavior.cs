using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_PlayerBehavior : MonoBehaviour
{
    public GameObject customGrid;
    GameObject moveTarget;
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


    private void Move()
    {
        // 마우스 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 포지션을 취득해서 대입
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            NK_CustomGrid gridScript = customGrid.GetComponent<NK_CustomGrid>();

            // 마우스 포지션에서 레이를 던져 물체 감지 시 hit에 대입
            if (Physics.Raycast(ray, out var hit))
            {
                // 오브젝트의 타겟 취득
                if (hit.collider.gameObject.CompareTag("Furniture"))
                {
                    moveTarget = hit.collider.gameObject.transform.parent.GetChild(0).gameObject;
                    gridScript.target = moveTarget;
                    gridScript.structure = hit.collider.gameObject;
                    moveTarget.SetActive(true);
                }
            }
        }
    }

    private void Clean()
    {
        // 청소도구를 플레이어에게 쥐게 한다
        // 청소하는 모션을 적용시킨다
        // - 맵에서 지저분한 물체가 청소도구와 닿으면 Destroy
    }

    private void Paint()
    {
        // 페인트롤을 플레이어에게 쥐게 한다
        // 페인트하는 모션을 적용시킨다
        // - 맵에서 페인트롤과 닿으면 벽의 색이 바뀐다
    }

    private void Sell()
    {
        // 가격 측정 기기를 플레이어에게 쥐게 한다
        // 기기를 가구에 댄다
        // 가격이 나온다
    }

    private void DemolishWall()
    {
        // 망치를 플레이어에게 쥐게 한다
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
