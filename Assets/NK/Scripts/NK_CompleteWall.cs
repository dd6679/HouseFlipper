using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.FilePathAttribute;

public class NK_CompleteWall : MonoBehaviour
{
    RaycastHit hit;

    public string location;
    public string color;
    public string colorCode;
    public GameObject[] walls;
    int currentCount = 0;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.tag.Contains("Wall") && GameManager.instance.myChangeTool.index == (int)NK_ChangeTool.ToolState.PaintTool && GameManager.instance.myChangeTool.isMoving)
                {
                    int count = 0;

                    foreach (GameObject wall in walls)
                    {
                        MeshRenderer mesh = wall.GetComponent<MeshRenderer>();
                        if (mesh.material.name.Contains(colorCode))
                        {
                            count++;
                            currentCount = count;
                        }
                    }

                    if (count == walls.Length)
                    {
                        NK_QuestUI.completeQuest[location].Add(color + "으로 페인트 칠하기");
                    }
                }
            }
        }
        NK_QuestUI.quests[location][color + "으로 페인트 칠하기"] = 100 / walls.Length * currentCount;
    }
}
