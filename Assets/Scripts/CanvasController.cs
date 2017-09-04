using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{

    public GameObject WebCamScreen;
    public GameObject CenterLine;
    public GameObject DebugPanel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ActiveControl();
    }

    void ActiveControl()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WebCamScreen.SetActive(!WebCamScreen.active);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CenterLine.SetActive(!CenterLine.active);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DebugPanel.SetActive(!DebugPanel.active);
        }
    }
}
