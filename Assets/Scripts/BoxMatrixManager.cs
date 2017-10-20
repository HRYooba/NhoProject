using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMatrixManager : MonoBehaviour
{
    public WebCamManager WebCamManager;
    public GameObject BoxPrefab;
    private int Width;
    private int Height;
    private float CompressionRatio;
    public float Level3D = 100;
    private GameObject[] BoxObject;

    // Use this for initialization
    void Start()
    {
        CompressionRatio = WebCamManager.compressionRatio;
        Width = (int)(WebCamManager.width * CompressionRatio);
        Height = (int)(WebCamManager.height * CompressionRatio);

        BoxObject = new GameObject[Width * Height];

        for (int y = 0; y < Height; y ++)
        {
            for (int x = 0; x < Width; x ++)
            {
                BoxObject[y * Width + x] = (GameObject)Instantiate(BoxPrefab, new Vector3((x - Width / 2), (y - Height / 2), 0) + transform.position, Quaternion.identity);
                BoxObject[y * Width + x].transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y < Height; y ++)
        {
            for (int x = 0; x < Width; x ++)
            {
                if (WebCamManager.checkActivePixel(x, y))
                {
                    BoxObject[y * Width + x].transform.localScale = new Vector3(BoxPrefab.transform.localScale.x, BoxPrefab.transform.localScale.y, Level3D);
                }
                else
                {
                    BoxObject[y * Width + x].transform.localScale = BoxPrefab.transform.localScale;
                }
            }
        }
    }
}
