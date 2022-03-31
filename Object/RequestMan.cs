using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestMan : MonoBehaviour
{
    private RequestMen requestMen;

    // Start is called before the first frame update
    void Start()
    {
        requestMen = GetComponentInParent<RequestMen>();
    }

    private void OnMouseDown()
    {
        requestMen.ShowRequestUI();
    }
}
