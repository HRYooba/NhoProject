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
    private Texture2D testTexture;
    private Color32[] webcamPixels;
    private Color32[] webcamPixelsBuffer;
    private bool[] activePixelList;

    void Start()
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

        webcamPixels = new Color32[Width * Height];
        webcamPixelsBuffer = new Color32[Width * Height];

        activePixelList = new bool[Width * Height];
    }

    void Update()
    {
        webcamPixels = webcamTexture.GetPixels32();
        if (DifferenceMode)
        {
            UpdateDifferenceMode();
        }
        else
        {
            UpdateNomalMode();
        }
    }

    void UpdateDifferenceMode()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                int r = webcamPixels[y * Width + x].r - webcamPixelsBuffer[y * Width + x].r;
                int g = webcamPixels[y * Width + x].g - webcamPixelsBuffer[y * Width + x].g;
                int b = webcamPixels[y * Width + x].b - webcamPixelsBuffer[y * Width + x].b;
                if (Mathf.Sqrt(r * r + g * g + b * b) < Threshold)
                {
                    activePixelList[y * Width + x] = false;
                }
                else
                {
                    activePixelList[y * Width + x] = true;
                }
            }
        }
        webcamPixelsBuffer = webcamTexture.GetPixels32();
    }

    void UpdateNomalMode()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                int r = webcamPixels[y * Width + x].r;
                int g = webcamPixels[y * Width + x].g;
                int b = webcamPixels[y * Width + x].b;
                if (Mathf.Sqrt(
                    (r - RecognizeColor.r) * (r - RecognizeColor.r) +
                    (g - RecognizeColor.g) * (g - RecognizeColor.g) +
                    (b - RecognizeColor.b) * (b - RecognizeColor.b))
                    < Threshold)
                {
                    activePixelList[y * Width + x] = true;
                }
                else
                {
                    activePixelList[y * Width + x] = false;
                }
            }
        }
    }

    public bool checkActivePixel(int index)
    {
        return activePixelList[index];
    }
}