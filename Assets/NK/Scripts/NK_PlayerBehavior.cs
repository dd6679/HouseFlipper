using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_PlayerBehavior : MonoBehaviour
{
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
        
    }

    // 상태 변화 관리 함수
    void ChangeState(PlayerBehaviorState state)
    {
        behaviorState = state;
        print(behaviorState + " 상태입니다.");
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
