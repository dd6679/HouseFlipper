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

    // �÷��̾� �ൿ ����
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
            // ���콺 �������� ����ؼ� ����
            Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
            //NK_CustomGrid gridScript = customGrid.GetComponent<NK_CustomGrid>();

            // ���콺 �����ǿ��� ���̸� ���� ��ü ���� �� hit�� ����
            if (Physics.Raycast(ray, out var hit))
            {
                // ������Ʈ�� Ÿ�� ���
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
                // �������� ��� ���� �ű�� ��� Ȱ��ȭ
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
        // û�ҵ����� �÷��̾�� ��� �Ѵ�
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.CleanTool);
        // ���콺 Ŭ�� ���� �� û���ϴ� ����� �����Ų��
        // - �ʿ��� �������� ��ü�� û�ҵ����� ������ Destroy
    }

    private void Paint()
    {
        // ����Ʈ���� �÷��̾�� ��� �Ѵ�
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.PaintTool);
        // ����Ʈ�ϴ� ����� �����Ų��
        // - �ʿ��� ����Ʈ�Ѱ� ������ ���� ���� �ٲ��
    }

    private void Sell()
    {
        // ���� ���� ��⸦ �÷��̾�� ��� �Ѵ�
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.SellTool);
        // ��⸦ ������ ���
        // ������ ���´�
    }

    private void DemolishWall()
    {
        // ��ġ�� �÷��̾�� ��� �Ѵ�
        changeTool.SwitchTool((int)NK_ChangeTool.ToolState.DemolishTool);
        // �μ��� ����� �����Ų��
        // - �ʿ��� ��ġ�� ������ Demolish
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

    // ���� ��ȭ ���� �Լ�
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
        print(behaviorState + " �����Դϴ�.");

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

    // ȭ������
    public void OnClickFlame()
    {
        ChangeState(PlayerBehaviorState.Flame);
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
