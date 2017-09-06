using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{

    public GameObject[] HUDObjects;

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
            HUDObjects[0].SetActive(!HUDObjects[0].active);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HUDObjects[1].SetActive(!HUDObjects[1].active);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HUDObjects[2].SetActive(!HUDObjects[2].active);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HUDObjects[3].SetActive(!HUDObjects[3].active);
        }
    }
}
