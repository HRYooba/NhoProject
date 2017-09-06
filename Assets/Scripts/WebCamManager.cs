using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamManager : MonoBehaviour
{
    [HeaderAttribute("WebCamera Setting")]
    public int Width = 640;
    public int Height = 480;
    public int FPS = 30;
    public int DeviceNum = 0;

    [Space(10), Range(0.1f, 1)]
    public float CompressionRatio;
    public RawImage WebCameraScreen;
    public RawImage ROIScreen;
    public Material ROI;

    private WebCamTexture webcamTexture;

    private bool[] activePixelList;
    
    // use ROI
    private Texture2D nowTexture;
    private Texture2D pastTexture;

    // shader result
    private RenderTexture resultReTexture;
    private Texture2D resultTexture;

    void Start()
    {
        SetupWebCamera();
        SetupRawImages();

        RenderTexture.active = resultReTexture;
        resultTexture = new Texture2D(resultReTexture.width, resultReTexture.height);
        resultTexture.ReadPixels(new Rect(0, 0, resultTexture.width, resultTexture.height), 0, 0);

        StartCoroutine(ROICoroutine());
    }

    void SetupWebCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("device num = " + i + ", name = " + devices[i].name);
        }
        webcamTexture = new WebCamTexture(devices[DeviceNum].name, Width, Height, FPS);
        webcamTexture.Play();
    }

    void SetupRawImages()
    {
        nowTexture = new Texture2D(Width, Height);
        pastTexture = new Texture2D(Width, Height);
        ROI.SetTexture("_MainTex", nowTexture);
        ROI.SetTexture("_BufferTex", pastTexture);

        resultReTexture = new RenderTexture((int)(Width * CompressionRatio), (int)(Height * CompressionRatio), 24);

        WebCameraScreen.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, Height);
        WebCameraScreen.texture = webcamTexture;

        ROIScreen.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, Height);
        ROIScreen.texture = resultReTexture;
    }

    void Update()
    {
        Graphics.Blit(ROI.mainTexture, resultReTexture, ROI);
    }

    // Region of interests function
    IEnumerator ROICoroutine()
    {
        while(true)
        {
            nowTexture.SetPixels32(webcamTexture.GetPixels32());
            pastTexture.SetPixels32(webcamTexture.GetPixels32());
            nowTexture.Apply();
            yield return new WaitForEndOfFrame();
            nowTexture.SetPixels32(webcamTexture.GetPixels32());
            nowTexture.Apply();
            pastTexture.Apply();

            RenderTexture.active = resultReTexture;
            resultTexture = new Texture2D(resultReTexture.width, resultReTexture.height);
            resultTexture.ReadPixels(new Rect(0, 0, resultTexture.width, resultTexture.height), 0, 0);
        }
    }

    public bool checkActivePixel(int x, int y)
    {
        Color c = resultTexture.GetPixel(x, y);

        if (c.r == 1.0f)
        {
            return true;
        }

        return false;
    }
}