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
        // ���콺 �������� ����ؼ� ����
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        NK_CustomGrid gridScript = customGrid.GetComponent<NK_CustomGrid>();

        // ���콺 �����ǿ��� ���̸� ���� ��ü ���� �� hit�� ����
        if (Physics.Raycast(ray, out var hit))
        {
            // ������Ʈ�� Ÿ�� ���
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
        // û�ҵ����� �÷��̾�� ��� �Ѵ�
        NK_ChangeTool.instance.SwitchTool((int)NK_ChangeTool.ToolState.CleanTool);
        // û���ϴ� ����� �����Ų��
        // - �ʿ��� �������� ��ü�� û�ҵ����� ������ Destroy
    }

    private void Paint()
    {
        // ����Ʈ���� �÷��̾�� ��� �Ѵ�
        NK_ChangeTool.instance.SwitchTool((int)NK_ChangeTool.ToolState.PaintTool);
        // ����Ʈ�ϴ� ����� �����Ų��
        // - �ʿ��� ����Ʈ�Ѱ� ������ ���� ���� �ٲ��
    }

    private void Sell()
    {
        // ���� ���� ��⸦ �÷��̾�� ��� �Ѵ�
        NK_ChangeTool.instance.SwitchTool((int)NK_ChangeTool.ToolState.SellTool);
        // ��⸦ ������ ���
        // ������ ���´�
    }

    private void DemolishWall()
    {
        // ��ġ�� �÷��̾�� ��� �Ѵ�
        NK_ChangeTool.instance.SwitchTool((int)NK_ChangeTool.ToolState.DemolishTool);
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
