using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGenerator : MonoBehaviour {

    public WebCamManager WebCamManager;
    public GameObject CollisionObjectPrefab;
    public int Width = 640;
    public int Height = 480;
    public int BetweenSpace = 10;

    private GameObject[] CollisionObject;

    // Use this for initialization
    void Start()
    {
        //CollisionObject = new GameObject[Width * Height];

        //for (int y = 0; y < Height; y += BetweenSpace)
        //{
        //    for (int x = 0; x < Width; x += BetweenSpace)
        //    {
        //        CollisionObject[y * Width + x] = (GameObject)Instantiate(CollisionObjectPrefab, new Vector3((x - Width / 2) / BetweenSpace, (y - Height / 2) / BetweenSpace, 0) + transform.position, Quaternion.identity);
        //        CollisionObject[y * Width + x].transform.parent = transform;
        //        CollisionObject[y * Width + x].transform.localScale = new Vector3(0, 0, 0);
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //for (int y = 0; y < Height; y += BetweenSpace)
        //{
        //    for (int x = 0; x < Width; x += BetweenSpace)
        //    {
        //        if (WebCamManager.checkActivePixel(y * Width + x))
        //        {
        //            CollisionObject[y * Width + x].transform.localScale = new Vector3(1, 1, 100);
        //        } else
        //        {
        //            CollisionObject[y * Width + x].transform.localScale = new Vector3(0, 0, 0);
        //        }
        //    }
        //}

        CollisionGenerate();
    }

    void CollisionGenerate()
    {
        for (int y = 0; y < Height; y += BetweenSpace)
        {
            for (int x = 0; x < Width; x += BetweenSpace)
            {
                if (WebCamManager.checkActivePixel(y * Width + x))
                {
                    GameObject collisionObject = (GameObject)Instantiate(CollisionObjectPrefab, new Vector3((x - Width / 2) / BetweenSpace, (y - Height / 2) / BetweenSpace, 0) + transform.position, CollisionObjectPrefab.transform.rotation);
                    collisionObject.transform.parent = transform;
                    Destroy(collisionObject, 0.1f);
                }
            }
        }
    }
}
