// Shatter Toolkit
// Copyright 2015 Gustav Olsson
using UnityEngine;

namespace ShatterToolkit.Helpers
{
    public class MouseShatter : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (Vector3.Distance(hit.collider.gameObject.transform.position, transform.position) <= 1.8f && hit.collider.gameObject.tag.Contains("Wall"))
                    {
                        Rigidbody rig = hit.collider.gameObject.GetComponent<Rigidbody>();
                        rig.isKinematic = false;
                        hit.collider.SendMessage("Shatter", hit.point, SendMessageOptions.DontRequireReceiver);

                    }
                }
            }
        }
    }
}