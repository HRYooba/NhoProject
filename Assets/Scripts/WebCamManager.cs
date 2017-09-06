using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamManager : MonoBehaviour
{
    public int Width = 640;
    public int Height = 480;
    public int FPS = 30;
    public int DeviceNum = 0;
    public RawImage RawImage;
    public Color32 RecognizeColor;
    public float Threshold;
    public bool DifferenceMode;

    private WebCamTexture webcamTexture;
    private bool[] activePixelList;

    private Texture2D nowTexture;
    private Texture2D pastTexture;
    public Material ROI;
    public GameObject obj;

    void Start()
    {
        SetupWebCamera();

        activePixelList = new bool[Width * Height];

        nowTexture = new Texture2D(Width, Height);
        pastTexture = new Texture2D(Width, Height);
        ROI.SetTexture("_NowTex", nowTexture);
        ROI.SetTexture("_PastTex", pastTexture);

        obj.GetComponent<Renderer>().material = ROI;

        StartCoroutine(ShaderCoroutine());
    }

    void SetupWebCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("device num = " + i + ", name = " + devices[i].name);
        }
        webcamTexture = new WebCamTexture(devices[DeviceNum].name, Width, Height, FPS);
        RawImage.texture = webcamTexture;
        RawImage.material.mainTexture = webcamTexture;
        RawImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, Height);
        webcamTexture.Play();
    }

    void Update()
    {

    }

    IEnumerator ShaderCoroutine()
    {
        while(true)
        {
            nowTexture.SetPixels32(webcamTexture.GetPixels32());
            pastTexture.SetPixels32(webcamTexture.GetPixels32());
            nowTexture.Apply();
            yield return null;
            pastTexture.Apply();
        }
    }

    public bool checkActivePixel(int index)
    {
        return activePixelList[index];
    }
}