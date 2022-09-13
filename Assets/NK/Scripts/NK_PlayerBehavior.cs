using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_PlayerBehavior : MonoBehaviour
{
    public GameObject customGrid;
    GameObject moveTarget;
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
        // ���콺 Ŭ�� ��
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 �������� ����ؼ� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            NK_CustomGrid gridScript = customGrid.GetComponent<NK_CustomGrid>();

            // ���콺 �����ǿ��� ���̸� ���� ��ü ���� �� hit�� ����
            if (Physics.Raycast(ray, out var hit))
            {
                // ������Ʈ�� Ÿ�� ���
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
        // û�ҵ����� �÷��̾�� ��� �Ѵ�
        // û���ϴ� ����� �����Ų��
        // - �ʿ��� �������� ��ü�� û�ҵ����� ������ Destroy
    }

    private void Paint()
    {
        // ����Ʈ���� �÷��̾�� ��� �Ѵ�
        // ����Ʈ�ϴ� ����� �����Ų��
        // - �ʿ��� ����Ʈ�Ѱ� ������ ���� ���� �ٲ��
    }

    private void Sell()
    {
        // ���� ���� ��⸦ �÷��̾�� ��� �Ѵ�
        // ��⸦ ������ ���
        // ������ ���´�
    }

    private void DemolishWall()
    {
        // ��ġ�� �÷��̾�� ��� �Ѵ�
        // �μ��� ����� �����Ų��
        // - �ʿ��� ��ġ�� ������ Demolish
    }

    // ���� ��ȭ ���� �Լ�
    void ChangeState(PlayerBehaviorState state)
    {
        behaviorState = state;
        print(behaviorState + " �����Դϴ�.");
        if (moveTarget != null)
        {
            moveTarget.SetActive(false);

        }
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
