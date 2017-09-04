using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour {

    public Text EyePos;
    public Text FPS;
    public GameObject EyeCamera;

    private int frameCount;
    private float prevTime;
    private float fps;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CalculateFps();

        EyePos.text = "EyePos : " + EyeCamera.transform.position.ToString();
        FPS.text = "FPS : " + fps;
    }

    void CalculateFps ()
    {
        ++frameCount;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.1f)
        {
            fps = frameCount / time; 

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
}
