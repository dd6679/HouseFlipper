using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_Move : MonoBehaviour
{
    public Transform camPos;
    GameObject objectHitPosition;
    RaycastHit hitRay, hitLayerMask;

    private void OnMouseUp()
    {
        transform.parent = null;
        Destroy(objectHitPosition);
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitRay))
        {
            objectHitPosition = new GameObject("HitPosition");
            objectHitPosition.transform.position = hitRay.point;
            transform.SetParent(objectHitPosition.transform);
        }
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("Floor");

        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
        {
            float H = Camera.main.transform.position.y;
            float h = objectHitPosition.transform.position.y;

            Vector3 newPos = (hitLayerMask.point * (H - h) + Camera.main.transform.position * h) / H;

            objectHitPosition.transform.position = newPos;
        }
    }
}
