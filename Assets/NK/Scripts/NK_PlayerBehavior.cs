using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_PlayerBehavior : MonoBehaviour
{
    // �÷��̾� �ൿ ����
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

    // ���� ��ȭ ���� �Լ�
    void ChangeState(PlayerBehaviorState state)
    {
        behaviorState = state;
        print(behaviorState + " �����Դϴ�.");
    }

    // �ű��
    public void OnClickMove()
    {
        ChangeState(PlayerBehaviorState.Move);
    }

    // û���ϱ�
    public void OnClickClean()
    {
        ChangeState(PlayerBehaviorState.Clean);
    }

    // ĥ�ϱ�
    public void OnClickPaint()
    {
        ChangeState(PlayerBehaviorState.Paint);
    }

    // �ȱ�
    public void OnClickSell()
    {
        ChangeState(PlayerBehaviorState.Sell);
    }

    // �� �ø���
    public void OnClickBuildWall()
    {
        ChangeState(PlayerBehaviorState.BuildWall);
    }

    // �ι溸 �ø���
    public void OnClickBuildLintel()
    {
        ChangeState(PlayerBehaviorState.BuildLintel);
    }

    // �� ö���ϱ�
    public void OnClickDemolishWall()
    {
        ChangeState(PlayerBehaviorState.DemolishWall);
    }

    // Ÿ�� �� �г��۾��ϱ�
    public void OnClickWorkTileAndPanel()
    {
        ChangeState(PlayerBehaviorState.WorkTileAndPanel);
    }
}
