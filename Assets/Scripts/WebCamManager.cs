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

    [Space(10)]
    public RawImage WebCameraScreen;
    public RawImage ROIScreen;
    public Material ROI;

    private WebCamTexture webcamTexture;
    private bool[] activePixelList;

    private Texture2D nowTexture;
    private Texture2D pastTexture;

    private RenderTexture shaderDestTexture;

    void Start()
    {
        SetupWebCamera();

        activePixelList = new bool[Width * Height];

        nowTexture = new Texture2D(Width, Height);
        pastTexture = new Texture2D(Width, Height);
        ROI.SetTexture("_MainTex", nowTexture);
        ROI.SetTexture("_BufferTex", pastTexture);

        shaderDestTexture = new RenderTexture(Width, Height, 24);

        WebCameraScreen.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, Height);
        WebCameraScreen.texture = webcamTexture;

        ROIScreen.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, Height);
        ROIScreen.texture = shaderDestTexture;

        StartCoroutine(CreateROICoroutine());
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

    void Update()
    {
        Graphics.Blit(ROI.mainTexture, shaderDestTexture, ROI);
    }

    IEnumerator CreateROICoroutine()
    {
        while(true)
        {
            nowTexture.SetPixels32(webcamTexture.GetPixels32());
            pastTexture.SetPixels32(webcamTexture.GetPixels32());
            nowTexture.Apply();
            yield return null;
            nowTexture.SetPixels32(webcamTexture.GetPixels32());
            nowTexture.Apply();
            pastTexture.Apply();
        }
    }

    public bool checkActivePixel(int index)
    {
        return activePixelList[index];
    }
}