using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGenerator : MonoBehaviour
{

    public WebCamManager WebCamManager;
    public GameObject CollisionObjectPrefab;
    private int Width;
    private int Height;
    private float CompressionRatio;

    private GameObject[] CollisionObject;

    // Use this for initialization
    void Start()
    {
        CompressionRatio = WebCamManager.compressionRatio;
        Width = (int)(WebCamManager.width * CompressionRatio);
        Height = (int)(WebCamManager.height * CompressionRatio);

        CollisionObject = new GameObject[Width * Height];

        for (int y = 0; y < Height; y ++)
        {
            for (int x = 0; x < Width; x ++)
            {
                CollisionObject[y * Width + x] = (GameObject)Instantiate(CollisionObjectPrefab, new Vector3((x - Width / 2), (y - Height / 2), 0) + transform.position, Quaternion.identity);
                CollisionObject[y * Width + x].transform.parent = transform;
                CollisionObject[y * Width + x].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CollisionGenerate();
    }

    void CollisionGenerate()
    {
        for (int y = 0; y < Height; y ++)
        {
            for (int x = 0; x < Width; x ++)
            {
                if (WebCamManager.checkActivePixel(x, y))
                {
                    CollisionObject[y * Width + x].SetActive(true);
                }
                else
                {
                    CollisionObject[y * Width + x].SetActive(false);
                }
            }
        }
    }
}
