using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_OutlineController : MonoBehaviour
{
    Outline outline;
    private void OnTriggerEnter(Collider other)
    {
        int layerMask = 1 << LayerMask.NameToLayer("House");
        if (other.gameObject.CompareTag("Furniture") || other.gameObject.layer == layerMask)
        {
            outline = gameObject.GetComponent<Outline>();
            outline.OutlineColor = Color.red;

        }
    }
}
