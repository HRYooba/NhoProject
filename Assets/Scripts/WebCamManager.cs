using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamManager : MonoBehaviour
{
    [HeaderAttribute("WebCamera Setting")]
    public int width = 640;
    public int height = 480;
    public int FPS = 30;
    public int deviceNum = 0;

    [Space(10), Range(0.1f, 1)]
    public float compressionRatio;
    public RawImage webCameraScreen;
    public RawImage resultScreen;
    public Material ROI;
    public Material imageBinarize;
    public bool isBinarize = false;

    private WebCamTexture webcamTexture;

    private bool[] activePixelList;

    // use shaders
    private Texture2D nowTexture;
    private Texture2D pastTexture;

    // shader result
    private int resultWidth;
    private int resultHeight;
    private RenderTexture resultReTexture;
    private Texture2D resultTexture;
    private Color[] resultColor;

    void Start()
    {
        resultWidth = (int)(width * compressionRatio);
        resultHeight = (int)(height * compressionRatio);

        SetupWebCamera();
        SetupRawImages();

        RenderTexture.active = resultReTexture;
        resultTexture = new Texture2D(resultReTexture.width, resultReTexture.height);
        resultTexture.ReadPixels(new Rect(0, 0, resultTexture.width, resultTexture.height), 0, 0);
        resultColor = new Color[resultWidth * resultHeight];

        StartCoroutine(ROICoroutine());
    }

    void SetupWebCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("device num = " + i + ", name = " + devices[i].name);
        }
        webcamTexture = new WebCamTexture(devices[deviceNum].name, width, height, FPS);
        webcamTexture.Play();
    }

    void SetupRawImages()
    {
        nowTexture = new Texture2D(width, height);
        pastTexture = new Texture2D(width, height);

        ROI.SetTexture("_MainTex", nowTexture);
        ROI.SetTexture("_BufferTex", pastTexture);
        imageBinarize.SetTexture("_MainTex", nowTexture);

        resultReTexture = new RenderTexture(resultWidth, resultHeight, 24);

        webCameraScreen.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        webCameraScreen.texture = webcamTexture;

        resultScreen.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        resultScreen.texture = resultReTexture;
    }

    void Update()
    {
        if (isBinarize)
        {
            Graphics.Blit(ROI.mainTexture, resultReTexture, imageBinarize);
        }
        else
        {
            Graphics.Blit(ROI.mainTexture, resultReTexture, ROI);
        }
    }

    // Region of interests function
    IEnumerator ROICoroutine()
    {
        while (webcamTexture.width <= 16 && webcamTexture.height <= 16)
        {
            yield return null;
        }

        while (true)
        {
            // Debug.Log(webcamTexture.width + ", " + nowTexture.width);
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
            resultColor = resultTexture.GetPixels();
        }
    }

    public bool checkActivePixel(int x, int y)
    {
        Color checkColor;
        if (isBinarize)
        {
            checkColor = Color.white;
        }
        else
        {
            checkColor = Color.red;
        }

        if (resultColor[x + resultWidth * y] == checkColor)
        {
            return true;
        }

        return false;
    }
}