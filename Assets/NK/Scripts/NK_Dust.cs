using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_Dust : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckObjectIsCamera(gameObject))
        {
/*            if (NK_ChangeTool.instance.index == (int)NK_ChangeTool.ToolState.CleanTool && NK_ChangeTool.instance.isMoving)
            {
                Destroy(gameObject);
            }*/
        }
    }

    bool CheckObjectIsCamera(GameObject target)
    {
        Camera cam = Camera.main;
        Vector3 screenPoint = cam.WorldToViewportPoint(target.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }
}
